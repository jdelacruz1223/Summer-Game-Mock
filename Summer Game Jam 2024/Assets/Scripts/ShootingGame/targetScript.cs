using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class targetScript : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject manager;
    public GameObject bottleSpawner;
    [Range(0,5)]
    public float fireRate = 1f;
    [Range(1,12)]
    public int ammoCap = 6;
    [Range(0,5)]
    public float reloadDelay = 2f;
    private bool isGameActive;
    private int score;
    [SerializeField]
    private int scorePerBottle = 10;
    private int numBullets;
    private float nextFire;
    private int shotsFired;
    private int shotsHit;
    private float Accuracy;
    private GameObject bottle;
    private DebugManager debugManager;
    private BottleSpawnController spawnController;
    private Vector3 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
        debugManager = manager.GetComponent<DebugManager>();
        spawnController = bottleSpawner.GetComponent<BottleSpawnController>();
        debugManager.setBottleDestroyedCount(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition; 
        Ray ray = gameCamera.ScreenPointToRay(mousePos);
        if (isGameActive) {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire && numBullets > 0) {
                shotsFired++;
                numBullets--;
                nextFire = Time.time + fireRate;
                Debug.DrawRay(gameCamera.transform.position, ray.direction * 100, Color.red, 2f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    bottle = hit.collider.gameObject;
                    if (bottle.CompareTag("Bottle")) {
                        shotsHit++;
                        score += scorePerBottle;
                        debugManager.increaseBottleDestroyedCount(1);
                        spawnController.destroyBottle(bottle.transform.parent.gameObject);
                        Destroy(bottle.transform.parent.gameObject);
                        Debug.Log("A bottle has been hit! Score: " + score);
                    } else {
                        Debug.Log("Miss!");
                    }
                } else {
                    Debug.Log("Miss!");
                }
                Accuracy = (shotsFired != 0) ? (float) shotsHit / shotsFired : 0f;
                Debug.Log("Accuracy: " + Accuracy * 100 + "%"); 
            } else if (Input.GetMouseButtonDown(0) && numBullets <= 0) {
                Debug.Log("Out of ammo! Press R to reload!");
            }
            if (Input.GetKeyDown(KeyCode.R) && numBullets < ammoCap) {
                numBullets = ammoCap;
                nextFire = Time.time + reloadDelay;
                Debug.Log("Reloading!");
            }
            
        }
    }
    public void setMinigameActive(bool b) {
        isGameActive = b;
        if (isGameActive) {
            score = 0;
            numBullets = ammoCap;
            shotsFired = 0;
            shotsHit = 0;
            score = 0;
            Debug.Log("Game Start!");
        } else {
            Debug.Log("Game is already active!");
        }
    }
    public bool isMinigameActive() {
        return isGameActive;
    }
    public void setScore(int s) {
        score = s;
    }
    public int getScore() {
        return score;
    }
    public int getNumBullets() {
        return numBullets;
    }
}
