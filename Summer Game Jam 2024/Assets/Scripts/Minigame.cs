using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGame : MonoBehaviour
{
    // Reference to the UI canvas for the prompt
    public GameObject promptCanvas;

    // RNG settings for spacebar presses
    public int minPresses = 10;
    public int maxPresses = 20;

    // Reference to the congratulatory message text
    public Text congratulationText;

    // Internal variables
    private int requiredPresses;
    private int currentPresses = 0;
    private bool isMiniGameActive = false;
    private bool isWaitingForConfirmation = false;

    // Start the mini-game
    public void StartMiniGame()
    {
        if (!isMiniGameActive)
        {
            requiredPresses = Random.Range(minPresses, maxPresses + 1);

            Debug.Log("Starting mini-game. Required presses: " + requiredPresses);

            // Show prompt UI above player's head
            promptCanvas.SetActive(true);

            isMiniGameActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMiniGameActive && !isWaitingForConfirmation && Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            Debug.Log("Spacebar pressed. Current presses: " + currentPresses);

            // Example: Update UI to show currentPresses / requiredPresses

            if (currentPresses >= requiredPresses)
            {
                Debug.Log("Mini-game completed!");
                isWaitingForConfirmation = true;
                StartCoroutine(WaitForConfirmation());
            }
        }
    }

    // Coroutine to wait for player confirmation
    IEnumerator WaitForConfirmation()
    {
        // Display congratulatory message
        congratulationText.gameObject.SetActive(true);
        congratulationText.text = "Congratulations, you win!\nPress F to continue.";

        // Wait for player confirmation
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                break;
            }
            yield return null;
        }

        // Deactivate congratulatory message after confirmation
        congratulationText.gameObject.SetActive(false);

        // Reset mini-game variables and UI elements for next playthrough
        ResetMiniGame();
    }

    // Function to end the mini-game
    void ResetMiniGame()
    {
        promptCanvas.SetActive(false);
        isMiniGameActive = false;
        isWaitingForConfirmation = false;
        currentPresses = 0;
    }
}
