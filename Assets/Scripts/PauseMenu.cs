using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    [Header("PauseMenu")]
    [SerializeField]
    private GameObject pauseMenu;
    public bool GetPauseMenuEnabled() {
        if(pauseMenu.activeSelf) {
            return true;
        }
        return false;
    }
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private KeyCode pauseMenuKey;
    public void SetPauseMenuKey() {
        string PauseMenyKey = PlayerPrefs.GetString("pause_Menu", "Escape");
        pauseMenuKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PauseMenyKey);
    }
    public KeyCode GetPauseMenyKey() {
        return pauseMenuKey;
    }
    [SerializeField]
    private bool freezeBackgroudGame = false;
    [SerializeField]
    private bool hideCursor = true;
    [SerializeField]
    private GameObject unsavedGameWarning;
    private bool canSwitchPauseMenu = true;

    [Header("Screenshot")]
    [SerializeField]
    private KeyCode screenshotKey;
    public void SetScreenshotKey() {
        string ScreenshotKey = PlayerPrefs.GetString("screenshot", "P");
        screenshotKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ScreenshotKey);
    }
    private int superSize;
    private bool canScreenshot = true;

    public void Load() {
        DisablePauseMenu();
        SetPauseMenuKey();
        SetScreenshotKey();
    }

    private void LateUpdate() {

        if(Input.GetKeyDown(pauseMenuKey) && canSwitchPauseMenu) {
            canSwitchPauseMenu = false;
            string currentSceneName = sceneLoader.GetCurrentSceneName();
            if(CheckMenu()) {
                return;
            }
            if(pauseMenu.activeSelf) {
                DisablePauseMenu();
            }
            else {
                EnablePauseMenu();
            }
        }
        if(Input.GetKeyDown(screenshotKey) && canScreenshot) {
            canScreenshot = false;
            superSize = PlayerPrefs.GetInt("video_screenshotSuperSize");
            ShotScreenshot();
            canScreenshot = true;
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
        canSwitchPauseMenu = true;

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
        if(hideCursor && !CheckMenu()) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        canSwitchPauseMenu = true;

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

        //sceneLoader.SetLoadingBackground(0);
        sceneLoader.LoadSceneFromName("MainMenu");
        unsavedGameWarning.SetActive(false);
        DisablePauseMenu();

    }
    #endregion

    private bool CheckMenu() {
        string currentSceneName = sceneLoader.GetCurrentSceneName();
        if(currentSceneName == "MainMenu" || currentSceneName == "LoadMainMenu" || currentSceneName == "Credits") {
            return true;
        }
        return false;
    }

    //Screenshot
    private void ShotScreenshot() {
        string currentDate = System.DateTime.Now.ToString();
        currentDate = currentDate.Replace("/", "-").Replace(":", "-").Replace(" ", "_");
        ScreenCapture.CaptureScreenshot("Koa_" + currentDate + ".png", superSize);
        Debug.Log("Screenshot - Koa_" + currentDate + ".png under size " + superSize);
    }

}
