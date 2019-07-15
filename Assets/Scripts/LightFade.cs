using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    public float speed;

    private float originalIntensity;
    private Light thisLight;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
        originalIntensity = thisLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        thisLight.intensity = (Mathf.Cos(Time.time * speed) + 1) / 2 * originalIntensity;
    }
}
