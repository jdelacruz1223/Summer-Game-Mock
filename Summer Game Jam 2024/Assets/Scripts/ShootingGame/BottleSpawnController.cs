using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawnController : MonoBehaviour
{
    public enum SpawnState
    {
        active,
        inactive
    }
    public SpawnState currentState;
    [SerializeField] private GameObject BottleToSpawn;
    [SerializeField] private GameObject[] Spawnpoints;
    [SerializeField] private int spawnDelay = 2;
    [SerializeField] private int maxBottles = 4;
    private List<GameObject> activeBottles = new List<GameObject>();
    
    private int currentSpawnpointIndex;
    private bool isSpawning = false;

    void Start()
    {
        currentState = SpawnState.inactive;
    }

    void Update()
    {
        if(checkBottle() && currentState == SpawnState.active)
        {
            StartCoroutine(SpawnBottleCoroutine());
        }
    }

    private IEnumerator SpawnBottleCoroutine()
    {
        Debug.Log("start");
        isSpawning = true;

        if(currentSpawnpointIndex == Spawnpoints.Length)
        {
            currentSpawnpointIndex = 0;
        }

        GameObject NewBottle = Instantiate
        (
            BottleToSpawn, 
            Spawnpoints[currentSpawnpointIndex].transform.position, 
            Quaternion.identity
        );

        activeBottles.Add(NewBottle);
        currentSpawnpointIndex++;

        yield return new WaitForSeconds(spawnDelay);
        isSpawning = false;
        Debug.Log("stop");
    }

    bool checkBottle()
    {
        //Debug.Log("");
        if(activeBottles.Count < maxBottles && !isSpawning)
        {
            return true;
        }
        else return false;
    }
    public void destroyBottle(GameObject bottle){
        activeBottles.Remove(bottle);
    }
}
