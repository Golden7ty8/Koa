using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    [Header("PauseMenu")]
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private KeyCode pauseMenuKey;
    [SerializeField]
    private bool freezeBackgroudGame = false;
    [SerializeField]
    private bool hideCursor = true;
    [SerializeField]
    private GameObject unsavedGameWarning;

    [Header("Screenshot")]
    [SerializeField]
    private KeyCode screenshotKey;
    private int superSize;

    private void Start() {
        DisablePauseMenu();
    }

    private void LateUpdate() {

        if(Input.GetKeyDown(pauseMenuKey)) {
            string currentSceneName = sceneLoader.GetCurrentSceneName();
            if(currentSceneName == "MainMenu" || currentSceneName == "LoadMainMenu" || currentSceneName == "Credits") {
                return;
            }
            if(pauseMenu.activeSelf) {
                DisablePauseMenu();
            }
            else {
                EnablePauseMenu();
            }
        }
        if(Input.GetKeyDown(screenshotKey)) {
            superSize = PlayerPrefs.GetInt("video_screenshotSuperSize");
            ShotScreenshot();
        }
        
    }

    //Pause menu
    #region
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
        options.SetActive(false);
        unsavedGameWarning.SetActive(false);
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
        unsavedGameWarning.SetActive(false);
        DisablePauseMenu();

    }
    #endregion

    //Screenshot
    private void ShotScreenshot() {
        string currentDate = System.DateTime.Now.ToString();
        currentDate = currentDate.Replace("/", "-").Replace(":", "-").Replace(" ", "_");
        ScreenCapture.CaptureScreenshot("Koa_" + currentDate + ".png", superSize);
    }

}
