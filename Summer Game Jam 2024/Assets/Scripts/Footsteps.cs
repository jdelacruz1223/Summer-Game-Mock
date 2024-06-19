using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footsteps;

    void Update()
    {
        // Check if any of the WASD keys are pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // If the footsteps sound is not playing, start playing it
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            // If none of the keys are pressed, stop the footsteps sound
            footsteps.Stop();
        }
    }
}
