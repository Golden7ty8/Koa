using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private KeyCode pauseMenuKey;
    [SerializeField]
    private bool freezeBackgroudGame = false;
    [SerializeField]
    private bool hideCursor = true;
    [SerializeField]
    private GameObject unsavedGameWarning;

    private void Start() {
        DisablePauseMenu();
    }

    private void LateUpdate() {

        if(Input.GetKeyDown(pauseMenuKey)) {
            if(pauseMenu.activeSelf) {
                DisablePauseMenu();
            }
            else {
                EnablePauseMenu();
            }
        }
        
    }

    public void EnablePauseMenu() {

        if(freezeBackgroudGame) {
            Time.timeScale = 0;
            //Pause game audios
        }
        else {
            //Disable controls and IA ?
        }
        pauseMenu.SetActive(true);
        if(hideCursor) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void DisablePauseMenu() {

        if(freezeBackgroudGame) {
            Time.timeScale = 1;
            //Continue game audios
        }
        else {
            //Enable controls and IA ?
        }
        pauseMenu.SetActive(false);
        if(hideCursor) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void CallMainMenu() {

        //if(game saved recently since a short interval of time and nothing important happend) {
            //MainMenu();
        //}
        //else {
            unsavedGameWarning.SetActive(true);
        //}

    }

    public void MainMenu() {

        sceneLoader.SetLoadingBackground(0);
        sceneLoader.LoadSceneFromName("MainMenu");

    }

}
