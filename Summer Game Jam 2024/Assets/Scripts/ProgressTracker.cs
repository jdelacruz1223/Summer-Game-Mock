using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public float totalDistance = 100.0f;  // Total distance to be covered
    public float speed = 1.0f;            // Speed of progress
    private float currentProgress = 0.0f; // Current progress

    public Text progressText;             // UI Text to display progress
    public Slider progressBar;            // UI Slider to visualize progress

    void Update()
    {
        // Update progress based on speed and time
        currentProgress += speed * Time.deltaTime;

        // Clamp progress to total distance
        currentProgress = Mathf.Clamp(currentProgress, 0, totalDistance);

        // Update UI elements
        UpdateProgressUI();
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
        {
            int progressPercentage = Mathf.RoundToInt((currentProgress / totalDistance) * 100.0f);
            progressText.text = "Progress: " + progressPercentage.ToString() + "mi";
        }

        if (progressBar != null)
        {
            progressBar.value = currentProgress / totalDistance;
        }
    }

    // Function to get the current progress percentage
    public float GetProgressPercentage()
    {
        return (currentProgress / totalDistance) * 100.0f;
    }
}
