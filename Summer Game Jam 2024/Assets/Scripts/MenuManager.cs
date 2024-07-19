using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject controlsPanel;
    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject partyNextButton;
    [SerializeField] private SceneTransition sceneTransition;
    private GameObject currentPanel;


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

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        currentPanel = controlsPanel;
        Debug.Log("Controls");
    }

    public void ClosePanel()
    {
        if(currentPanel == controlsPanel) controlsPanel.SetActive(false);
        else if(currentPanel == settingsPanel) settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        currentPanel = settingsPanel;
        Debug.Log("Settings");
    }
}
