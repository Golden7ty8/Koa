using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioClipPlus {

    public AudioClip clip;
    public float volume;

}

public class AmbiantSoundsManager : MonoBehaviour
{

    public AudioClipPlus[] waterDripSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            int index = Random.Range(0, waterDripSounds.Length);
            GetComponent<AudioSource>().clip = waterDripSounds[index].clip;
            GetComponent<AudioSource>().volume = waterDripSounds[index].volume;
            GetComponent<AudioSource>().Play();
        }
    }
}
