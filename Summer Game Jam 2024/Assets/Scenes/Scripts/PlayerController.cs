using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;
    
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer)){
            if (hit.collider != null){
                Vector3 movPos = transform.position;
                movPos.y = hit.point.y + groundDist;
                transform.position = movPos;
            }
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 movDir = new Vector3(x, 0, y);
        rb.velocity = movDir * speed;


        if (x != 0 || y != 0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);

        if (x != 0 && x < 0)
            sr.flipX = true;

        else if (x != 0 && x > 0)
            sr.flipX = false;
    }
}
