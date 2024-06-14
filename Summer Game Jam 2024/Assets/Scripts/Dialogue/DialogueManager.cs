using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using Assets.Scripts.Dialogue;

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

    public bool randomEncounterPlaying { get; set; }

    private Story currentStory;

    PlayerInputActions playerControls;
    
    private static DialogueManager instance;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();

        if (instance != null) Debug.LogWarning("Another instance of the DialogueManager is running.");
        instance = this;
    }

    public static DialogueManager GetInstance() { return instance; }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) return;


        if (playerControls.Controls.Interact.triggered)
        {
            if (!dialoguePanel.activeSelf)
            {
                choicesText = new TextMeshProUGUI[choices.Length];
                int index = 0;

                foreach (GameObject choice in encountersChoices)
                {
                    choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                    index++;
                }
            }

            ContinueStory();
            DialogueChoices.GetInstance().ParseTag(currentStory);
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
    }

    public void EnterEncounterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        randomEncounterPlaying = true;
        encountersPanel.SetActive(true);
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;

        if (!randomEncounterPlaying)
        { 
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            if (currentStory.canContinue)
            {
                dialogueText.text = currentStory.Continue();
                DisplayChoices();
            }
        } else {
            encountersPanel.SetActive(false);
            randomEncounterPlaying = false;
            encounterText.text = "";

            if (currentStory.canContinue)
            {
                encounterText.text = currentStory.Continue();
                DisplayEncounterChoices();
            }

        }
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
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        var choice_button = choices[0].gameObject;
        if (randomEncounterPlaying) choice_button = encountersChoices[0].gameObject;
        EventSystem.current.SetSelectedGameObject(choice_button);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
