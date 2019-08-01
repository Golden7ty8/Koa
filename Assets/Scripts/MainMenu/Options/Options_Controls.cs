using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Options_Controls : MonoBehaviour {

    //[Header("Controls options")]
    //General
    //Movements
    //Abilities
    [SerializeField]
    private GameObject bindingInfo;

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

    public void UpdatePauseMenuKey() {
        StartCoroutine(WaitPauseMenuKey());
    }

    private IEnumerator WaitPauseMenuKey() {
        while(bindingInfo.activeSelf) {
            Debug.Log("Wait");
            yield return new WaitForFixedUpdate();
        }
        AdvancedGameUI.instance.GetPauseMenu().SetPauseMenuKey();
    }

    public void UpdateScreenshotKey() {
        StartCoroutine(WaitScreenshotKey());
    }

    private IEnumerator WaitScreenshotKey() {
        while (bindingInfo.activeSelf) {
            Debug.Log("Wait");
            yield return new WaitForFixedUpdate();
        }
        AdvancedGameUI.instance.GetPauseMenu().SetScreenshotKey();
    }
}
