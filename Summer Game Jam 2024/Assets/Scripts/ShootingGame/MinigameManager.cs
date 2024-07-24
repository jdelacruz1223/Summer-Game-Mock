using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text timerText;
    public UnityEngine.UI.Text ammoText;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public GameObject bottleSpawner;
    private float currentTime;
    [SerializeField]
    private float timeLimit = 45f;
    private bool gameFinished;
    private targetScript target;
    private BottleSpawnController spawnController;
    

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.GetComponent<targetScript>();
        spawnController = bottleSpawner.GetComponent<BottleSpawnController>();
        timerText.text = "Time Left: " + timeLimit;
        gameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.isMinigameActive()) {
            if (currentTime < timeLimit) {
                currentTime += Time.deltaTime;
                timerText.text = (timeLimit - currentTime >= 0) ? "Time Left: " + ((int) (timeLimit - currentTime)) : "Time Left: 0";
            } else {
                gameOverScreen.SetActive(true);
                target.setMinigameActive(false);
                gameFinished = true;
                spawnController.currentState = BottleSpawnController.SpawnState.inactive;
            }
            scoreText.text = "Score: " + target.getScore();
            ammoText.text = "Ammo: " + target.getNumBullets() + "/" + target.ammoCap;
        } else if (!gameFinished){
            startScreen.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)) {
                target.setMinigameActive(true);
                startScreen.SetActive(false);
                spawnController.currentState = BottleSpawnController.SpawnState.active;
            }
        }
    }
}
