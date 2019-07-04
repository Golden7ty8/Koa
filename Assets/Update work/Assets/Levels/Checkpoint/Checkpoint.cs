using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    [Header("Asset")]
    [SerializeField]
    private GameObject checkpointModel;

    [Header("Custom")]
    [SerializeField]
    private bool isEnabled;
    [SerializeField]
    private Color color_Disabled;
    [SerializeField]
    private Color color_Enabled;
    [SerializeField]
    private Vector3 enableEffectPosition;
    [SerializeField]
    private GameObject enableEffect;

    private void Start() {
        
        if(isEnabled) {
            SetEnabled();
        }
        else {
            SetDisabled();
        }
        
    }

    private void OnCollisionEnter(Collision collision) {

        if(!isEnabled) {
            Instantiate(enableEffect, enableEffectPosition, transform.rotation);
            SetEnabled();
            //Play sound
        }
        else {
            //Play sound (softer ?)
        }
        
    }

    private void SetEnabled() {
        //Change color or Light material, I think it is easier to change the color (and emition color) but I don't saw quickly how to do it individually
    }

    private void SetDisabled() {
        //Change color or Light material, I think it is easier to change the color (and emition color) but I don't saw quickly how to do it individually
    }

}
