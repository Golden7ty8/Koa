using UnityEngine;
using System.Collections;

public class GameCore_Controls : MonoBehaviour {

    [SerializeField]
    private bool isReady = false;

    // ***x are the options not working or complete yet

    //Reset video options
    public void ResetControls(GameObject caller) {

        //General
        PlayerPrefs.SetString("pause_Menu", "Escape");
        PlayerPrefs.SetString("screenshot", "P");

        //Movements
        PlayerPrefs.SetString("walk_forward", "Z");
        PlayerPrefs.SetString("walk_forward_alt", "UpArrow");
        PlayerPrefs.SetString("walk_back", "S");
        PlayerPrefs.SetString("walk_back_alt", "DownArrow");
        PlayerPrefs.SetString("walk_left", "A");
        PlayerPrefs.SetString("walk_left_alt", "LeftArrow");
        PlayerPrefs.SetString("walk_right", "D");
        PlayerPrefs.SetString("walk_right_alt", "RightArrow");
        PlayerPrefs.SetString("crouch", "LeftControl");
        PlayerPrefs.SetString("crouch_alt", "None");
        PlayerPrefs.SetString("jump", "Space");
        PlayerPrefs.SetString("jump_alt", "None");
        PlayerPrefs.SetString("run", "LeftShift");
        PlayerPrefs.SetString("run_alt", "None");

        //Abiliies
        PlayerPrefs.SetString("black_light", "LeftAlt");
        PlayerPrefs.SetString("black_light_alt", "None");
        PlayerPrefs.SetString("charge", "S");
        PlayerPrefs.SetString("charge_alt", "None");

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
