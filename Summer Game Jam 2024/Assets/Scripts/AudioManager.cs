using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip backgroundMusicClip;  // Drag your background music clip here in the Inspector
    [Range(0f, 1f)]
    public float volume = 0.5f;  // Volume slider

    private AudioSource audioSource;

    void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Loop the background music
        audioSource.clip = backgroundMusicClip;
        audioSource.volume = volume; // Set the initial volume

        PlayBackgroundMusic(); // Start playing the background music
    }

    void Update()
    {
        // Update the volume in real-time
        audioSource.volume = volume;
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource.isPlaying) return; // If music is already playing, don't play it again
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }
}
