using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    public AudioSource cameraAudioSource;
    public AudioClip endMusicClip;

    private bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.tag == "Player")
        {
            EndGameFunc();
        }
    }

    void EndGameFunc() {
        active = false;
        cameraAudioSource.clip = endMusicClip;
        cameraAudioSource.Play();
    }
}
