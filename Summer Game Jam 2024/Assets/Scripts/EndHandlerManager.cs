using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndHandlerManager : MonoBehaviour
{
    public TextMeshProUGUI totalTimeTxt;
    public TextMeshProUGUI moneySavedTxt;
    public TextMeshProUGUI fishCaughtTxt;
    public TextMeshProUGUI encountersTxt;

    void Start()
    {
        var instance = Manager.GetInstance();

        float currentTime = instance.GetTotalTimeElapsed();
        int minutes = Mathf.FloorToInt(currentTime / 60); 
        int seconds = Mathf.FloorToInt(currentTime % 60); 

        totalTimeTxt.text = $"{minutes}:{seconds:00} minutes";
        moneySavedTxt.text = "$" + instance.currentMoney.ToString();
        fishCaughtTxt.text = instance.fishCaughtNum.ToString();
        encountersTxt.text = instance.encountersNum.ToString();
    }

    public void ReplayGame()
    {
        Manager.GetInstance().ReplayGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit the game
            #if UNITY_EDITOR
                // If in the Unity editor, stop playing
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                // If in a built application, quit
                Application.Quit();
            #endif
        }
    }
}
