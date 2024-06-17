using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    // Singleton instance
    static BackgroundMusic instance;

    // Drag in the .mp3 files here, in the editor
    public AudioClip[] MusicClips;

    // Reference to the AudioSource component
    public AudioSource Audio;

    void Awake()
    {
        // Singleton pattern to keep only one instance alive
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Determine which clip to play based on scene name
        AudioClip clipToPlay = null;

        switch (scene.name)
        {
            case "MainMenu":
                clipToPlay = MusicClips[0];
                break;
            case "Party":
                clipToPlay = MusicClips[1];
                break;
            case "MapScreen":
                clipToPlay = MusicClips[2];
                break;
            case "PlanningScreen":
                clipToPlay = MusicClips[3];
                break;
            case "TravelScene":
                clipToPlay = MusicClips[4];
                break;
            default:
                clipToPlay = MusicClips[2]; // Default clip if scene name doesn't match
                break;
        }

        // Only switch the music if it changed
        if (clipToPlay != null && clipToPlay != Audio.clip)
        {
            Audio.Stop();
            Audio.clip = clipToPlay;
            Audio.Play();
        }
    }
}
