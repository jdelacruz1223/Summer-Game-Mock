using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{


    public void StartGame()
    {
        //Debug.Log("StartGame method called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        //Debug.Log("EndGame method called");
        Application.Quit();
    }

    public void RestartGame()
    {
        //Debug.Log("RestartGame method called");
        SceneManager.LoadScene(0);
    }
}
