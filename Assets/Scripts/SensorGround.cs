using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : MonoBehaviour
{
    [Header("References:")]
    public Rigidbody rb;

    [Header("Options:")]
    [Tooltip("Actual height of Koa.")]
    public float KoaHeight;
    [Tooltip("0 means feet are only the bare bottom of Koa, 1 means all of Koa is considered as feet in terms of whether Koa can jump or not.")]
    public float feetRatio;

    //[HideInInspector]
    public bool isGrounded;

    private bool isCurrentlyGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Up:" + Time.deltaTime);
        if (!rb.IsSleeping())
        {
            isGrounded = isCurrentlyGrounded;
            isCurrentlyGrounded = false;
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Detected.");
        if (other.tag != "Player")
        {
            isGrounded = true;
            //Debug.Log(other.n)
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            isGrounded = false;
    }*/

    void OnCollisionStay(Collision other)
    {
        Debug.Log("Col:" + Time.deltaTime);

        for (int i = 0; i < other.contactCount; i++) {

            //Debug.Log(other.GetContact(i).point);

            float tmp = Mathf.Abs(other.GetContact(i).normal.x);

            if (other.collider.tag != "Player" && other.GetContact(i).point.y <= transform.position.y + KoaHeight * feetRatio && Mathf.Abs(other.GetContact(i).normal.x) <= other.GetContact(i).normal.y) {

                isCurrentlyGrounded = true;
                return;

            }

        }

        //isGrounded = false;
    }

    /*void OnCollisionExit(Collision other)
    {
        if (other.collider.tag != "Player")
            isGrounded = false;
    }*/

}
