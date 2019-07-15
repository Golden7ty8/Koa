using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Options_Video : MonoBehaviour {

    [Header("Video options")]
    //Language
    [SerializeField]
    private Dropdown languageDropdown;
    //Displaye mode
    [SerializeField]
    private Dropdown DisplayModeDropdown;
    //Resoultion
    private Resolution[] resolutions;
    [SerializeField]
    private Dropdown resolutionDropdown;
    private int actualResolutionIndex = 0;
    //Limit FPS
    [SerializeField]
    private Dropdown limitFPSDropdown;
    [SerializeField]
    private GameObject customLimitFPSObject;
    [SerializeField]
    private InputField customLimitFPSInputField;
    //Achievement notification
    [SerializeField]
    private Toggle achievementNotification;

    [Header("Advanced stats")]
    //Advanced stats
    //Show framerates
    [SerializeField]
    private Toggle showFrameratesToggle;
    //Show system clock
    [SerializeField]
    private Toggle showSystemClockToggle;

    [Header("Graphics quality")]
    //Graphics quality
    [SerializeField]
    private Dropdown graphicsQualityDropdown;
    [SerializeField]
    private GameObject graphicsQualityAdvancedObject;
    //Advanced graphics quality
    //[SerializeField]
    //private bool

    private void Start() {
        UpdateUI();
    }

    public void CallResetVideoOptions() {

        GameCore_Main.instance.GetComponent<GameCore_Video>().ResetVideoOptions(this.gameObject);
        //UpdateUI();

    }

    public void UpdateUI() {

        //Language
        languageDropdown.value = PlayerPrefs.GetInt("video_Language");
        languageDropdown.RefreshShownValue();

        //Resolution
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionsList = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) {
            string detectedResolution = resolutions[i].width + " x " + resolutions[i].height;
            resolutionsList.Add(detectedResolution);
            if(resolutions[i].width.ToString() == PlayerPrefs.GetString("video_Resolution_Width") && resolutions[i].height.ToString() == PlayerPrefs.GetString("video_Resolution_Height")) {
                actualResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.value = actualResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Display mode
        DisplayModeDropdown.value = PlayerPrefs.GetInt("video_DisplayModeIndex");
        DisplayModeDropdown.RefreshShownValue();

        //Limit FPS
        limitFPSDropdown.value = PlayerPrefs.GetInt("video_LimitFPSIndex");
        limitFPSDropdown.RefreshShownValue();
        if (limitFPSDropdown.value != 3) {
            customLimitFPSObject.SetActive(false);
        }
        else {
            customLimitFPSInputField.text = PlayerPrefs.GetInt("video_CustomLimitFPS").ToString();
            customLimitFPSObject.SetActive(true);
        }

        //Achievement notification
        if (PlayerPrefs.GetInt("video_achievementNotification") == 1) {
            achievementNotification.isOn = true;
        }
        else {
            achievementNotification.isOn = false;
        }

        //Advanced stats
        //Show framerates
        if (PlayerPrefs.GetString("video_ShowFramerates") == "true") {
            showFrameratesToggle.isOn = true;
        }
        else {
            showFrameratesToggle.isOn = false;
        }
        //Show system clock
        if(PlayerPrefs.GetString("video_ShowSystemClock") == "true") {
            showSystemClockToggle.isOn = true;
        }
        else {
            showSystemClockToggle.isOn = false;
        }

        //Graphics quality
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("video_GraphicsQuality");

    }

    //Options
    #region
    //Language
    public void SetLanguage(int languageIndex) {
        PlayerPrefs.SetInt("video_Language", languageIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetLanguage();

    }

    //Display mode
    public void SetDisplayMode(int displayModeIndex) {

        PlayerPrefs.SetInt("video_DisplayModeIndex", displayModeIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().ApplyScreenResolution();

    }

    //Resolution
    public void SetResolution(int resolutionIndex) {

        PlayerPrefs.SetString("video_Resolution_Width", resolutions[resolutionIndex].width.ToString());
        PlayerPrefs.SetString("video_Resolution_Height", resolutions[resolutionIndex].height.ToString());
        GameCore_Main.instance.GetComponent<GameCore_Video>().ApplyScreenResolution();

    }

    //Limit FPS
    #region
    public void SetLimitFPS(int limitFPSIndex) {
        PlayerPrefs.SetInt("video_LimitFPSIndex", limitFPSIndex);
        if (limitFPSDropdown.value != 3) {
            customLimitFPSObject.SetActive(false);

            if (limitFPSIndex == 0) {
                PlayerPrefs.SetInt("video_LimitFPS", -1);
            }
            if (limitFPSIndex == 1) {
                PlayerPrefs.SetInt("video_LimitFPS", 30);
            }
            if (limitFPSIndex == 2) {
                PlayerPrefs.SetInt("video_LimitFPS", 60);
            }
        }
        else {
            customLimitFPSInputField.text = PlayerPrefs.GetInt("video_CustomLimitFPS", PlayerPrefs.GetInt("video_LimitFPS")).ToString();
            customLimitFPSObject.SetActive(true);
        }
    }

    public void SetCustomLimitFPS(string customFPSLimit) {
        PlayerPrefs.SetInt("video_CustomLimitFPS", int.Parse(customFPSLimit));
    }
    #endregion

    //Achievement notification
    public void SetAchievementNotification(bool show) {
        if (show) {
            PlayerPrefs.SetInt("video_achievementNotification", 1);
        }
        else {
            PlayerPrefs.SetInt("video_achievementNotification", 0);
        }
    }

    //Advanced stats
    #region
    //Show framerates
    public void SetShowFramerates(bool showFramerates) {
        if(showFramerates) {
            PlayerPrefs.SetString("video_ShowFramerates", "true");
        }
        else {
            PlayerPrefs.SetString("video_ShowFramerates", "false");
        }
        AdvancedGameUI.instance.GetAdvancedStatsManager().UpdateAdvancedStatsUI();
    }

    //Show system clock
    public void SetShowSystemClock(bool showSystemClock) {
        if(showSystemClock) {
            PlayerPrefs.SetString("video_ShowSystemClock", "true");
        }
        else {
            PlayerPrefs.SetString("video_ShowSystemClock", "false");
        }
        AdvancedGameUI.instance.GetAdvancedStatsManager().UpdateAdvancedStatsUI();
    }
    #endregion

    //Graphics quality
    public void SetGraphicsQuality(int graphicsQualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality", graphicsQualityIndex);
        if(graphicsQualityIndex != 4) {
            graphicsQualityAdvancedObject.SetActive(false);
        }
        else {
            graphicsQualityAdvancedObject.SetActive(true);
        }
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality();
    }
    //Advanced graphics quality
    #region
    //Texture quality
    public void SetGraphicsQuality_TextureQuality(int qualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality_TextureQuality", qualityIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_TextureQuality();
    }
    #endregion
    #endregion

}
