using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using Assets.Scripts.Dialogue;
using Assets.Scripts;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public bool dialogueIsPlaying { get; private set; }

    [Header("Choices UI")]
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private GameObject[] choices;

    [Header("Encounter UI")]
    [SerializeField] private GameObject[] encountersChoices;
    [SerializeField] private GameObject encountersPanel;
    [SerializeField] private TextMeshProUGUI encounterText;
    [SerializeField] private TextMeshProUGUI encounterTitleText;

    public bool randomEncounterPlaying { get; set; }

    private Story currentStory;

    PlayerInputActions playerControls;
    
    private static DialogueManager instance;

    bool nowInDialogueMode;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();

        if (instance != null) Debug.LogWarning("Another instance of the DialogueManager is running.");
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        UIManager.GetInstance().ControlUI(dialoguePanel, false);

        // Ensure the EventSystem is available and set up early
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    IEnumerator triggerCheck()
    {
        Debug.Log("Trigger Checked");
       
        yield return new WaitForSeconds(1/2);
        nowInDialogueMode = false;
        Debug.Log("NowInDialogueMode set to False");
       
    }

    public static DialogueManager GetInstance() { return instance; }

    private void Update()
    {
        if (!dialogueIsPlaying) return;

        if (playerControls.Controls.Interact.triggered && !nowInDialogueMode)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Cursor.visible = false;
        nowInDialogueMode = true;
        StartCoroutine(triggerCheck());

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        randomEncounterPlaying = false;
        dialoguePanel.SetActive(true);
        UIManager.GetInstance().HideUI();

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;

            foreach (GameObject choice in choices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }

            // Display if there is
            DisplayChoices();
        }
    }

    public void EnterEncounterDialogueMode(TextAsset inkJSON)
    {
        //Cursor.visible = false;
        UIManager.GetInstance().HideUI();
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        randomEncounterPlaying = true;
        encountersPanel.SetActive(true);


        if (currentStory.canContinue)
        {
            encounterText.text = currentStory.Continue();

            choicesText = new TextMeshProUGUI[encountersChoices.Length];
            int index = 0;

            encounterTitleText.text = currentStory.globalTags[0];

            foreach (GameObject choice in encountersChoices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }

            // Display if there is
            DisplayEncounterChoices();
        }
    }

    void ExitDialogueMode()
    {
        if (!randomEncounterPlaying)
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            if (currentStory.canContinue)
            {
                dialogueText.text = currentStory.Continue();
                DisplayChoices();
            }
        } else {
            dialogueIsPlaying = false;
            encountersPanel.SetActive(false);
            encounterText.text = "";

            if (currentStory.canContinue)
            {
                encounterText.text = currentStory.Continue();
                DisplayEncounterChoices();
            }

            randomEncounterPlaying = false;
            RandomEncounterManager.GetInstance().currentlyInEncounter = false;
        }

        Cursor.visible = true;
        UIManager.GetInstance().RestoreUI();
        Debug.Log(currentStory.currentTags.ToString());
        DialogueChoices.GetInstance().ParseTag(currentStory);
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (!randomEncounterPlaying)
            {
                dialogueText.text = currentStory.Continue();
                DisplayChoices();
            }
            else
            {
                encounterText.text = currentStory.Continue();
                DisplayEncounterChoices();
            }

        }
        else
        {
            Invoke(nameof(ExitDialogueMode), 1/2) ;
        }
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    void DisplayEncounterChoices()
    {

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > encountersChoices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            encountersChoices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            Debug.Log(choice.text);
            index++;
        }

        for (int i = index; i < encountersChoices.Length; i++)
        {
            encountersChoices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        yield return new WaitForSeconds(0.5f);

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();

            var choiceButton = choices[0].gameObject;
            if (randomEncounterPlaying)
                choiceButton = encountersChoices[0].gameObject;

            EventSystem.current.SetSelectedGameObject(choiceButton);
        }
        else
        {
            Debug.LogWarning("EventSystem is not available.");
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
