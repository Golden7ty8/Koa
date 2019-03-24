using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : MonoBehaviour
{

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
            isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            isGrounded = false;
    }
}
