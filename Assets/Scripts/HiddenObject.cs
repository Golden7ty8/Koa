using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    static float fadeSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        Color tmp = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = new Color(tmp.r, tmp.g, tmp.b, 0);
        fadeSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = GetComponent<Renderer>().material.color;
        if (/*tmp.a < 1 && */tmp.a > 0) {
            GetComponent<Renderer>().material.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
        } else if (tmp.a > 1)
            GetComponent<Renderer>().material.color = new Color(tmp.r, tmp.g, tmp.b, 1);
        if (tmp.a < 0)
            GetComponent<Renderer>().material.color = new Color(tmp.r, tmp.g, tmp.b, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Blacklight")
        {
            GetComponent<Renderer>().material.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime * 2);
            Color tmp = GetComponent<Renderer>().material.color;

            if (GetComponent<Renderer>().material.color.a > 1)
                GetComponent<Renderer>().material.color = new Color(tmp.r, tmp.g, tmp.b, 1);

            Debug.Log("!");
        }
    }
}
