using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

    [Header("Keys")]
    //private KeyCode KeyPauseMenu;
    [Header("GameObject UI")]
    public GameObject pauseMenu;

    public void LateUpdate() {

        //if(Input.GetKeyDown(GameCore_Controls.gameCore_Controls_instance.PauseMenu)) {
        //    togglePauseMenu();
        //}

    }

    public void togglePauseMenu() {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        if(Time.timeScale == 0f) {
            Time.timeScale = 1f;
            return;
        }
        else {
            Time.timeScale = 0f;
            return;
        }
    }

}
