using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class ProgressTracker : MonoBehaviour
{
    public float totalDistance = 100.0f;   // Total distance to be covered
    public float speed = 1.0f;             // Speed of progress
    private float currentProgress = 0.0f;  // Current progress

    public Text progressText;              // UI Text to display progress
    public Slider progressBar;             // UI Slider to visualize progress

    // List of scenes to load at 25%, 50%, 75%, and 100% progress
    public List<SceneReference> scenesToLoad = new List<SceneReference>();

    private HashSet<int> progressCheckpoints = new HashSet<int>();  // To track progress checkpoints
    private Dictionary<int, bool> sceneLoadedFlags = new Dictionary<int, bool>();  // Track if scenes are loaded

    void Start()
    {
        // Validate and sort scenesToLoad based on their progress percentage
        scenesToLoad.Sort((a, b) => a.progressPercentage.CompareTo(b.progressPercentage));

        // Store progress points in a hash set for quick lookup and initialize flags
        foreach (var sceneRef in scenesToLoad)
        {
            progressCheckpoints.Add(sceneRef.progressPercentage);
            sceneLoadedFlags[sceneRef.progressPercentage] = false;
        }

        // Load current progress and visited checkpoints from the data manager if it exists
        if (ProgressDataManager.Instance != null)
        {
            currentProgress = ProgressDataManager.Instance.CurrentProgress;
        }
    }

    void Update()
    {
        if (!RandomEncounterManager.GetInstance().currentlyInEncounter)
        {
            // Update progress based on speed and time
            currentProgress += speed * Time.deltaTime;

            // Check for progress checkpoints
            foreach (var checkpoint in progressCheckpoints)
            {
                if (currentProgress >= GetProgressForPercentage(checkpoint) && !sceneLoadedFlags[checkpoint])
                {
                    // Skip loading the scene if it has already been visited
                    if (ProgressDataManager.Instance != null && ProgressDataManager.Instance.VisitedCheckpoints.Contains(checkpoint))
                    {
                        continue;
                    }

                    // Save current progress and mark the scene as visited
                    if (ProgressDataManager.Instance != null)
                    {
                        ProgressDataManager.Instance.CurrentProgress = currentProgress;
                        ProgressDataManager.Instance.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        ProgressDataManager.Instance.VisitedCheckpoints.Add(checkpoint);
                    }

                    // Load corresponding scene
                    int sceneIndex = GetSceneIndexForProgress(checkpoint);
                    if (sceneIndex != -1)
                    {
                        sceneLoadedFlags[checkpoint] = true;  // Mark the scene as loaded
                        SceneManager.LoadScene(sceneIndex);
                    }
                }
            }

            // Clamp progress to total distance
            currentProgress = Mathf.Clamp(currentProgress, 0, totalDistance);

            // Update UI elements
            UpdateProgressUI();
        }
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
        {
            int progressPercentage = Mathf.RoundToInt(GetProgressPercentage());
            progressText.text = "Progress: " + progressPercentage.ToString() + "%";
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

    // Function to convert progress percentage to actual progress value
    private float GetProgressForPercentage(int percentage)
    {
        return totalDistance * percentage / 100.0f;
    }

    // Function to get the scene index associated with a progress percentage
    private int GetSceneIndexForProgress(int progress)
    {
        foreach (var sceneRef in scenesToLoad)
        {
            if (sceneRef.progressPercentage == progress)
            {
                return sceneRef.sceneIndex;
            }
        }
        return -1;  // Scene not found
    }

    // Optional: Method to reset scene load flags if needed
    public void ResetSceneLoadFlags()
    {
        foreach (var key in sceneLoadedFlags.Keys.ToList())
        {
            sceneLoadedFlags[key] = false;
        }
    }
}

[System.Serializable]
public class SceneReference
{
    public int progressPercentage;  // Progress percentage at which to load the scene
    public int sceneIndex;          // Scene index in Build Settings
}
