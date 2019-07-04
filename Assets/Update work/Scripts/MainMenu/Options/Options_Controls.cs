using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Options_Controls : MonoBehaviour {

    //[Header("Controls options")]
    //General
    //Movements
    //Abilities

    private void Start() {
        UpdateUI();
    }

    public void CallResetControlsOptions() {

        GameCore_Main.instance.GetComponent<GameCore_Controls>().ResetControls(this.gameObject);
        //UpdateUI();

    }

    public void UpdateUI() {

        //General

        //Movements

        //Abilities

    }
}
