using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{


    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    private bool playerInRange;

    PlayerInputActions playerActions;
    private void Awake()
    {
        playerActions = new PlayerInputActions();
        playerActions.Enable();

        visualCue.SetActive(false);
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if (playerActions.Controls.Interact.triggered)
            {
                // example for random encounters
                DialogueManager.GetInstance().randomEncounterPlaying = true;
                DialogueManager.GetInstance().EnterEncounterDialogueMode(inkJson);
                visualCue.SetActive(false);
            }
        }
        else visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInRange = false;
    }
}
