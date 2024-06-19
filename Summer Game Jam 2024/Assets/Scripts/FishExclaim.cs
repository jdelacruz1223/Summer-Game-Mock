using UnityEngine;

public class ExclamationMarkController : MonoBehaviour
{
    public GameObject exclamationMark; // Reference to the exclamation mark object
    public float proximityRadius = 5.0f; // Radius within which the exclamation mark appears

    private Transform player; // Reference to the player

    private void Start()
    {
        // Hide the exclamation mark at the start
        exclamationMark.SetActive(false);
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= proximityRadius)
        {
            ShowExclamationMark();
        }
        else
        {
            HideExclamationMark();
        }

        // Make the exclamation mark follow the object and face the camera
        if (exclamationMark.activeSelf)
        {
            exclamationMark.transform.position = transform.position + Vector3.up * 2; // Adjust the height as needed
            exclamationMark.transform.rotation = Camera.main.transform.rotation; // Face the camera
        }
    }

    // Call this method to show the exclamation mark
    public void ShowExclamationMark()
    {
        exclamationMark.SetActive(true);
    }

    // Call this method to hide the exclamation mark
    public void HideExclamationMark()
    {
        exclamationMark.SetActive(false);
    }
}
