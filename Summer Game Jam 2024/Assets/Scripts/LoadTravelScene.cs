using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTravelScene : MonoBehaviour
{
    // Function to load the travel scene by build index
    public void LoadTravel()
    {
        int travelSceneIndex = 4; // Replace with the actual build index of your travel scene

        // Reset scene load flags to ensure checkpoints can be revisited
        if (ProgressDataManager.Instance != null)
        {
            ProgressTracker progressTracker = FindObjectOfType<ProgressTracker>();
            if (progressTracker != null)
            {
                progressTracker.ResetSceneLoadFlags();
            }
        }

        SceneManager.LoadScene(travelSceneIndex);
    }
}
