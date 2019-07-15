using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCylinder : MonoBehaviour
{

    public ControllerPlayer controllerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            controllerPlayer.spawnPoint = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
        }
    }
}
