using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    [Tooltip("The thing the camera is following.")]
    public GameObject viewSubject;

    public Vector3 posOffset/*, rotOffset*/;

    // Start is called before the first frame update
    void Start()
    {
        //posOffset = transform.position - viewSubject.transform.position;
        //rotOffset = transform.eulerAngles - viewSubject.transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = viewSubject.transform.position + posOffset;
        //transform.eulerAngles = viewSubject.transform.eulerAngles + rotOffset;
    }
}
