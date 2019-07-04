using UnityEngine;
using System.Collections;

public class GameCore_Controls : MonoBehaviour {

    [SerializeField]
    private bool isReady = false;

    // ***x are the options not working or complete yet

    //Reset video options
    public void ResetControls(GameObject caller) {

        //General
        PlayerPrefs.SetString("pauseMenu", "Escape");
        PlayerPrefs.SetString("screenshot", "P");

        //Movements
        PlayerPrefs.SetString("walkForward", "Z");
        PlayerPrefs.SetString("walkBack", "S");
        PlayerPrefs.SetString("walkLeft", "Q");
        PlayerPrefs.SetString("walkRight", "D");
        PlayerPrefs.SetString("crouch", "LeftShift");
        PlayerPrefs.SetString("jump", "Space");

        //Abiliies
        PlayerPrefs.SetString("interact", "E");
        PlayerPrefs.SetString("darkLight", "Mouse0");

        Debug.Log("Controls should have been reset and saved at their default state");
        //caller.GetComponent<Options_Controls>().UpdateUI();

    }

    public void Load() {
        //If the game has never applied a first time default controls
        if(PlayerPrefs.GetString("ControlsAppliedFirstStart") != "true") {
            Debug.Log("Applying default controls");
            ResetControls(null);
            PlayerPrefs.SetString("ControlsAppliedFirstStart", "true");
        }
        else {
        }
        isReady = true;
    }

    public bool CheckIfReady() {
        if (isReady) {
            return true;
        }
        return false;
    }

}
