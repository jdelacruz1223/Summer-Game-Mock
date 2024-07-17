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
    private Vector3 mousePos;
    //public GameObject CrossHairs;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition; 
        Ray ray = gameCamera.ScreenPointToRay(mousePos);
        //target = gameCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gameCamera.nearClipPlane));
        if (Input.GetMouseButtonDown(0)) {
            Debug.DrawRay(target, ray.direction * 100, Color.red, 2f);
            //Debug.Log("Mouse position was at (" + target.x + ", " + target.y + ", " + target.z +")");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                bottle = hit.collider.gameObject;
                if (bottle.CompareTag("Bottle")) {
                    bottle.SetActive(false);
                    Debug.Log("A bottle has been hit!");
                } 
            } else {
                Debug.Log("Miss!");
            }
            
        }
    }
}
