using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

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
    }

    private void Update()
    {
        if (!dialogueIsPlaying) return;

        if (playerControls.Controls.Interact.triggered)
        {
            ContinueStory();
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = currentStory.Continue();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            Invoke(nameof(ExitDialogueMode), 1/2) ;
        }
    }
}
