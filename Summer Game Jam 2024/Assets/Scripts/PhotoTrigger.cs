using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PhotoTrigger : MonoBehaviour
{
    public GameObject photoPanel;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            photoPanel.SetActive(true);
        }
        else photoPanel.SetActive(false);
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

    public void TakePhoto()
    {
        PhotoCapture.GetInstance().takePicture();
        Destroy(this);
    }
}
