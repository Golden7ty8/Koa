using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{

    [Range(0, 20)]
    public float fallDelay = 2;
    [Range(0, 20)]
    public float reloadTime = 4;
    [Range(0, 20)]
    public float maxFallTime = 2;

    Rigidbody rb;
    float fallTimer;
    float reloadTimer;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;

        fallTimer = 0;
        reloadTimer = 0;

        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("fallTimer = " + fallTimer);
        if (fallTimer > 0)
        {
            //This is the delay part.
            fallTimer -= Time.deltaTime;
            if (fallTimer <= 0)
                rb.isKinematic = false;
        }
        else if (rb.isKinematic == false) {
            //This is the falling part.
            fallTimer -= Time.deltaTime;
            if (fallTimer <= maxFallTime * -1)
                BreakPlatform();
        } else if (fallTimer > reloadTime * -1) {
            fallTimer -= Time.deltaTime;
            if (fallTimer <= reloadTime * -1)
                ResetPlatform();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(rb.isKinematic == true)
                TriggerCollision();
        }
        else if (rb.isKinematic == false) {
            BreakPlatform();
        }
    }

    void TriggerCollision() {
        //Debug.Log("TriggerCollision ran.");
        fallTimer = fallDelay;
    }

    /*void TriggerFall() {
        rb.useGravity = true;
    }*/

    void BreakPlatform() {
        rb.isKinematic = true;
        //Hide platform.
        transform.position = new Vector3(0, -500, 0);
        fallTimer = 0;
    }

    void ResetPlatform() {
        transform.position = originalPos;
    }
}
