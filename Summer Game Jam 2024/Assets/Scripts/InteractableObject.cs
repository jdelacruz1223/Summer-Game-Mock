using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Reference to the mini-game script
    public MiniGame miniGame;

    void Update()
    {
        // Check if interaction key (F) is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interaction key (F) pressed."); // Check if this message appears in the Console

            // Call the interact method
            Interact();
        }
    }

    // Function to handle player interaction
    public void Interact()
    {
        Debug.Log("Interact method called."); // Check if this message appears in the Console

        // Check if mini-game script is assigned
        if (miniGame != null)
        {
            Debug.Log("Starting mini-game."); // Check if this message appears in the Console

            // Start the mini-game
            miniGame.StartMiniGame();
        }
        else
        {
            Debug.LogError("MiniGame script is not assigned to InteractableObject!");
        }
    }
}
