using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MiniGame : MonoBehaviour
{
    // Reference to the UI canvas for the prompt
    public GameObject promptCanvas;

    // Reference to the UI Slider for progress indication
    public Slider progressSlider;

    // Reference to the congratulatory message text
    public Text congratulationText;

    // Reference to the UI Image for displaying the fish image
    public Image fishImage;

    // Reference to the UI Text for the start message
    public Text startText;

    // RNG settings for spacebar presses
    public int minPresses = 10;
    public int maxPresses = 20;

    // List of fish textures to display
    public List<Sprite> fishSprites;

    // Internal variables
    private int requiredPresses;
    private int currentPresses = 0;
    private bool isMiniGameActive = false;
    private bool isWaitingForConfirmation = false;

    void Start()
    {
        // Ensure promptCanvas starts inactive
        promptCanvas.SetActive(false);
        startText.gameObject.SetActive(false); // Start text should initially be inactive
    }

    // Start the mini-game
    public void StartMiniGame()
    {
        if (!isMiniGameActive)
        {
            requiredPresses = Random.Range(minPresses, maxPresses + 1);

            Debug.Log("Starting mini-game. Required presses: " + requiredPresses);

            // Show start text
            startText.gameObject.SetActive(true);
            startText.text = "Get ready! Press Spacebar to catch the fish.";

            // Show prompt UI above player's head
            promptCanvas.SetActive(true);

            // Reset progress slider
            progressSlider.value = 0;

            isMiniGameActive = true;

            // Start coroutine to hide start text after a brief delay
            StartCoroutine(HideStartTextAfterDelay(3f)); // Hide after 3 seconds (adjust as needed)
        }
    }

    // Coroutine to hide start text after a delay
    IEnumerator HideStartTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
       // startText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMiniGameActive && !isWaitingForConfirmation && Input.GetKeyDown(KeyCode.Space))
        {

            currentPresses++;
            Debug.Log("Spacebar pressed. Current presses: " + currentPresses);

            // Update progress slider value
            float progress = (float)currentPresses / requiredPresses;
            progressSlider.value = progress;

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
        // Display congratulatory message with fish size
        Manager.GetInstance().increaseFishCaught();

        congratulationText.gameObject.SetActive(true);
        int fishSize = Random.Range(10, 50); // Random size between 10 and 49 centimeters
        congratulationText.text = "Congratulations, you caught a fish!\nSize: " + fishSize + " cm\nPress F to continue.";

        // Display random fish image
        //fishImage.gameObject.SetActive(true);
        int randomFishIndex = Random.Range(0, fishSprites.Count);
        fishImage.sprite = fishSprites[randomFishIndex];

        // Wait for player confirmation
        while (true)
        {
            startText.gameObject.SetActive(false);
            fishImage.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                break;
            }
            yield return null;
        }

        // Deactivate congratulatory message and fish image after confirmation
        congratulationText.gameObject.SetActive(false);
        fishImage.gameObject.SetActive(false);

        // Reset mini-game variables and UI elements for next playthrough
        ResetMiniGame();
    }

    // Function to reset mini-game variables and UI elements
    void ResetMiniGame()
    {
        promptCanvas.SetActive(false);
        isMiniGameActive = false;
        isWaitingForConfirmation = false;
        currentPresses = 0;
        progressSlider.value = 0;
    }
}
