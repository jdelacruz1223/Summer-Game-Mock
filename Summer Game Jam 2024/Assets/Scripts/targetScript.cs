using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class targetScript : MonoBehaviour
{
    public Camera gameCamera;
    private GameObject bottle;
    private Vector3 target;
    //public GameObject CrossHairs;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //target = Input.mousePosition;
        target = gameCamera.transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        //CrossHairs.transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) {
            Debug.DrawRay(gameCamera.transform.position, gameCamera.transform.forward * 100, Color.red, 2f);
            Debug.Log("Mouse position was at (" + target.x + ", " + target.y + ", " + target.z +")");
            RaycastHit hit;
            if (Physics.Raycast(target, gameCamera.transform.forward, out hit, Mathf.Infinity)) {
                bottle = hit.collider.gameObject;
                if (bottle != null) {
                    bottle.SetActive(false);
                    Debug.Log("A bottle has been hit!");
                } 
            } else {
                Debug.Log("Miss!");
            }
            
        }
    }
}
