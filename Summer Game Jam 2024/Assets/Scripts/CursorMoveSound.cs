using UnityEngine;

public class CursorMoveSound : MonoBehaviour
{
    public AudioSource cursorMoveSound;

    void Update()
    {
        // Check if W or S keys are pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            cursorMoveSound.Play();
        }
    }
}
