using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLight : MonoBehaviour
{

    [Header("Options")]
    public float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, spinSpeed * Time.deltaTime, 0);
    }
}
