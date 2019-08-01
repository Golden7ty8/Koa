using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    [Header("Asset")]
    public ControllerPlayer controllerPlayer;
    [SerializeField]
    private Light checkpointLight;
    [SerializeField]
    private Transform spawnPoint; 
    private Animator animator;

    [Header("Custom")]
    [SerializeField]
    private bool isEnabled;
    [SerializeField]
    private GameObject enableEffect;

    private void Start() {

        animator = GetComponent<Animator>();
        if(isEnabled) {
            StartCoroutine(SetEnabled());
        }
        else {
            SetDisabled();
        }
        
    }

    private void OnCollisionEnter(Collision collision) {

        if(!isEnabled) {
            isEnabled = true;
            enableEffect.SetActive(true);
            StartCoroutine(SetEnabled());
            controllerPlayer.spawnPoint = spawnPoint.position;
            //Play sound
        }
        else {
            //Play sound (softer ?)
        }
        
    }

    private IEnumerator SetEnabled() {
        animator.SetBool("Enable", true);
        yield return new WaitForSecondsRealtime(1.583f);
        checkpointLight.gameObject.SetActive(true);
    }

    private void SetDisabled() {
        enableEffect.SetActive(false);
        animator.SetBool("Enable", false);
        checkpointLight.gameObject.SetActive(false);
    }

}
