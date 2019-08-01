using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefsOnStart : MonoBehaviour
{

    public bool reset;

    // Start is called before the first frame update
    void Awake()
    {
        if(reset)
            PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
