using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantSoundsManager : MonoBehaviour
{

    public AudioClip[] waterDripSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = waterDripSounds[Random.Range(0, waterDripSounds.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
