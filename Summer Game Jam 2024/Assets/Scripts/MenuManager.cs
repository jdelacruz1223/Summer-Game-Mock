using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;

    public void StartGame()
    {
        // Use SceneTransition to transition to the game scene
        sceneTransition.TransitionToScene(1);
    }

    public void EndGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void RestartGame()
    {
        // Use SceneTransition to transition to the start menu scene (assuming index 0)
        sceneTransition.TransitionToScene(0);
    }

    public void NextScene()
    {
        // Use SceneTransition to transition to the next scene
        sceneTransition.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
