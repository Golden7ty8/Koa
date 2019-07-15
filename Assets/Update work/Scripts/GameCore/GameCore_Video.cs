using UnityEngine;
using System.Collections;

public class GameCore_Video : MonoBehaviour {

    [SerializeField]
    private bool isReady = false;

    // ***x are the options not working or complete yet

    //Reset video options
    public void ResetVideoOptions(GameObject caller) {

        //Language -> English
        PlayerPrefs.SetInt("video_Language", 0);
        SetLanguage();
        //Screen options -> Actual screen width and height and fullscreen
        PlayerPrefs.SetString("video_Resolution_Width", Screen.width.ToString());
        PlayerPrefs.SetString("video_Resolution_Height", Screen.height.ToString());
        PlayerPrefs.SetInt("video_DisplayModeIndex", 1);
        ApplyScreenResolution();
        //Limit FPS -> -1
        PlayerPrefs.GetInt("video_LimitFPSIndex", 0);
        PlayerPrefs.SetInt("video_LimitFPS", -1);
        SetLimitFPS();
        //Achievement notification -> 1/true
        PlayerPrefs.SetInt("video_achievementNotification", 1);
        //Advanced stats -> Hide
        PlayerPrefs.SetString("video_ShowFramerates", "false");
        PlayerPrefs.SetString("video_ShowSystemClock", "false");
        SetShowAdvancedStats();
        //***Graphics quality -> Medium
        PlayerPrefs.SetInt("video_GraphicsQuality", 1);
        SetGraphicsQuality();

        Debug.Log("Video options should have been reset and saved at their default state");
        if(caller != null) {
            caller.GetComponent<Options_Video>().UpdateUI();
        }

    }

    public void Load() {
        //If the game has never applied a first time default video options
        if(PlayerPrefs.GetString("VideoOptionsAppliedFirstStart") != "true") {
            Debug.Log("Applying default options for video options");
            ResetVideoOptions(null);
            PlayerPrefs.SetString("VideoOptionsAppliedFirstStart", "true");
        }
        else {
            SetLanguage();
            ApplyScreenResolution();
            SetShowAdvancedStats();
            SetGraphicsQuality();
        }
        isReady = true;
    }

    public bool CheckIfReady() {
        if (isReady) {
            return true;
        }
        return false;
    }

    //Language
    public void SetLanguage() {

        //English
        if(PlayerPrefs.GetInt("video_Language") == 0) {
            LocalizationManager.instance.LoadLocalizedText("localizationText_en");
        }
        //Français
        if (PlayerPrefs.GetInt("video_Language") == 1) {
            LocalizationManager.instance.LoadLocalizedText("localizationText_fr");
        }

    }

    //Screen resolution
    private Vector2 SetResolution() {

        int width = int.Parse(PlayerPrefs.GetString("video_Resolution_Width"));
        int height = int.Parse(PlayerPrefs.GetString("video_Resolution_Height"));
        return new Vector2(width, height);

    }
    //Screen Fullscreen
    private FullScreenMode SetDisplayMode() {

        int newFullscreenMode = PlayerPrefs.GetInt("video_DisplayModeIndex");
        FullScreenMode displayFullScreenMode = Screen.fullScreenMode;
        //Window
        if(newFullscreenMode == 0) {
            displayFullScreenMode = FullScreenMode.FullScreenWindow;
        }
        //Fullsceen
        if (newFullscreenMode == 1) {
            displayFullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        return displayFullScreenMode;

    }
    //Set Screen options
    public void ApplyScreenResolution() {

        int newWidth = (int)SetResolution().x;
        int newHeight = (int)SetResolution().y;
        FullScreenMode newFullScreenMode = SetDisplayMode();
        Screen.SetResolution(newWidth, newHeight, newFullScreenMode);

    }

    //Limit FPS
    public void SetLimitFPS() {
        if(PlayerPrefs.GetInt("video_LimitFPSIndex") != 3) {
            Application.targetFrameRate = PlayerPrefs.GetInt("video_LimitFPS");
        }
        else {
            Application.targetFrameRate = PlayerPrefs.GetInt("video_CustomLimitFPS");
        }
    }

    //Advanced stats
    public void SetShowAdvancedStats() {

        AdvancedGameUI.instance.GetAdvancedStatsManager().UpdateAdvancedStatsUI();

    }

    //***Graphics quality
    #region
    public void SetGraphicsQuality() {

        //Low
        if(PlayerPrefs.GetInt("video_GraphicsQuality") == 0) {

        }
        //Medium
        //High
        //Ultra

    }

    public void SetGraphicsQuality_TextureQuality() {

    }
    #endregion

}
