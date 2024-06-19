using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private RectTransform fader;

    void Start()
    {
        // Ensure the fader is active when the game starts
        fader.gameObject.SetActive(true);
    }

    // This method will be called to transition to a new scene
    public void TransitionToScene(int sceneIndex)
    {
        // Ensure the fader is active before transitioning
        fader.gameObject.SetActive(true);

        // Scale transition: scale out to hide the scene
        fader.localScale = Vector3.zero;
        LeanTween.scale(fader, Vector3.one * 2f, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            // Load the new scene after the transition completes
            if (sceneIndex == 4) Manager.GetInstance().StartTimer();

            SceneManager.LoadScene(sceneIndex);
        });
    }

    // This method can be used for any other custom scene transitions if needed
    public void CustomTransitionToScene(int sceneIndex)
    {
        // Implement your custom transition here using LeanTween or other methods
        SceneManager.LoadScene(sceneIndex);
    }
}
