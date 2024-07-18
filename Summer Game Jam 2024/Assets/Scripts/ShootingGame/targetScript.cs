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
    [Range(0,5)]
    public float fireRate = 1f;
    [Range(1,12)]
    public int ammoCap = 6;
    [Range(0,5)]
    public float reloadDelay = 2f;
    private int numBullets;
    private float nextFire;
    private int shotsFired;
    private int shotsHit;
    private float Accuracy;
    private GameObject bottle;
    private DebugManager debugManager;
    private Vector3 target;
    private Vector3 mousePos;
    //public GameObject CrossHairs;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        shotsFired = 0;
        shotsHit = 0;
        numBullets = ammoCap;
        debugManager = manager.GetComponent<DebugManager>();
        debugManager.setBottleDestroyedCount(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition; 
        Ray ray = gameCamera.ScreenPointToRay(mousePos);
        //target = gameCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gameCamera.nearClipPlane));
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && numBullets > 0) {
            shotsFired++;
            numBullets--;
            nextFire = Time.time + fireRate;
            Debug.DrawRay(gameCamera.transform.position, ray.direction * 100, Color.red, 2f);
            //Debug.Log("Mouse position was at (" + target.x + ", " + target.y + ", " + target.z +")");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                bottle = hit.collider.gameObject;
                if (bottle.CompareTag("Bottle")) {
                    shotsHit++;
                    debugManager.increaseBottleDestroyedCount(1);
                    Destroy(bottle.transform.parent.gameObject);
                    Debug.Log("A bottle has been hit!");
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
        if (Input.GetKeyDown(KeyCode.R)) {
            numBullets = ammoCap;
            nextFire = Time.time + reloadDelay;
            Debug.Log("Reloading!");
        }
    }
}
