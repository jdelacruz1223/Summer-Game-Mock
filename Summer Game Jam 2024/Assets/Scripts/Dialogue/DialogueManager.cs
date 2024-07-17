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
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public bool dialogueIsPlaying { get; set; }

    [Header("Choices UI")]
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private GameObject[] choices;

    [Header("Encounter UI")]
    [SerializeField] private GameObject[] encountersChoices;

    [SerializeField] private GameObject encountersPanel;
    [SerializeField] private TextMeshProUGUI encounterText;
    [SerializeField] private TextMeshProUGUI encounterTitleText;

    [SerializeField] private GameObject noItemPanel;
    [SerializeField] private TextMeshProUGUI noItemDescriptionText;


    public bool randomEncounterPlaying { get; set; }
    bool hasNoItem { get; set; }

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
        if (DebugManager.GetInstance() != null) if (!DebugManager.GetInstance().dialogueManager) { Destroy(this.gameObject); return; };

        dialogueIsPlaying = false;
        hasNoItem = false;
        UIManager.GetInstance().ControlUI(dialoguePanel, false);
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
        currentStory = new Story(inkJSON.text);

        if (Manager.GetInstance().party.Count == 0 && currentStory.globalTags[1].Split(" ")[1] != "false")
        {
            RandomEncounterManager.GetInstance().currentlyInEncounter = false;
            return;
        }

        Cursor.visible = false;
        UIManager.GetInstance().HideUI();
        dialogueIsPlaying = true;
        randomEncounterPlaying = true;
        encountersPanel.SetActive(true);

        currentStory.BindExternalFunction("setItem", (string item) =>
        {
            StartCoroutine(CheckVariables(item));
        });

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

            if (currentStory.canContinue && hasNoItem == false)
            {
                encounterText.text = currentStory.Continue();
                DisplayEncounterChoices();
            }

            randomEncounterPlaying = false;
            currentStory.UnbindExternalFunction("setItem");
            RandomEncounterManager.GetInstance().currentlyInEncounter = false;
        }

        Cursor.visible = true;
        UIManager.GetInstance().RestoreUI();
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
                string nextLine = currentStory.Continue();

                if (nextLine.Equals("") && !currentStory.canContinue)
                {
                    Invoke(nameof(ExitDialogueMode), 1 / 2);
                } else
                {
                    encounterText.text = nextLine;
                    DisplayEncounterChoices();
                }
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
            index++;
        }

        for (int i = index; i < encountersChoices.Length; i++)
        {
            encountersChoices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectEncounterChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

      
        var choiceButton = choices[0].gameObject;
        EventSystem.current.SetSelectedGameObject(choiceButton);
    }

    private IEnumerator SelectEncounterChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();


        var choiceButton = encountersChoices[0].gameObject;
        EventSystem.current.SetSelectedGameObject(choiceButton);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }

    IEnumerator CheckVariables(string item)
    {
        string prefix = item.Split(" ")[0];
        string param = item.Split(" ")[1];
        string text = $"You do not have any {prefix} left! Dealt DMG to you!";

        int val = 0;

        switch (prefix)
        {
            case "tires":
                if (param.Contains("-"))
                {
                    val = Manager.GetInstance().tiresNum + int.Parse(param);
                    Debug.Log("VALUE: " + val);

                    if (val >= 0)
                    {
                        Manager.GetInstance().decreaseTireCount(-int.Parse(param));
                        yield break; // Exit coroutine
                    }

                    HandleNoItemScenario(text);
                }
                break;

            case "food":
                if (param.Contains("-"))
                {
                    val = Manager.GetInstance().snacksNum + int.Parse(param);
                    Debug.Log("VALUE: " + val);

                    if (val >= 0)
                    {
                        Manager.GetInstance().decreaseSnackCount(-int.Parse(param));
                        yield break; // Exit coroutine
                    }

                    HandleNoItemScenario(text);
                }
                break;

            case "medicine":
                if (param.Contains("-"))
                {
                    val = Manager.GetInstance().medicineNum + int.Parse(param);
                    Debug.Log("VALUE: " + val);

                    if (val >= 0)
                    {
                        Manager.GetInstance().decreaseMedicineCount(-int.Parse(param));
                        yield break; // Exit coroutine
                    }

                    HandleNoItemScenario(text);
                }
                break;
        }

        yield return new WaitForSeconds(3);
        hasNoItem = false;
        noItemPanel.SetActive(false);
        noItemDescriptionText.text = "";
        RandomEncounterManager.GetInstance().currentlyInEncounter = false;
    }

    private void HandleNoItemScenario(string text)
    {
        hasNoItem = true;
        ExitDialogueMode();
        RandomEncounterManager.GetInstance().currentlyInEncounter = true;
        Manager.GetInstance().decreaseUserHealth(5);

        if (Manager.GetInstance().party.Count > 0)
        {
            text += " and to your party!";
            Manager.GetInstance().changeHealthToParty(-5);
        }

        noItemPanel.SetActive(true);
        noItemDescriptionText.text = text;
    }
}
