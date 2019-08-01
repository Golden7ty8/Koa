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
    [SerializeField]
    private GameObject limitFPSVSyncEnabledMessage;
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
    [SerializeField]
    private Dropdown modelsQualityDropdown;
    [SerializeField]
    private Dropdown texturesQualityDropdown;
    [SerializeField]
    private Dropdown shadowsQualityDropdown;
    [SerializeField]
    private Dropdown particulesQualityDropdown;
    [SerializeField]
    private Toggle reflectionsQualityToggle;
    [SerializeField]
    private Dropdown AntialiasingQualityDropdown;
    [SerializeField]
    private Toggle vsyncQualityToggle;
    //Screenshot
    [SerializeField]
    private Dropdown screenshotQualityDropdown;

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
        if(limitFPSDropdown.value != 3) {
            customLimitFPSObject.SetActive(false);
        }
        else {
            customLimitFPSInputField.text = PlayerPrefs.GetInt("video_CustomLimitFPS").ToString();
            customLimitFPSObject.SetActive(true);
        }
        if(limitFPSDropdown.value != 0) {
            vsyncQualityToggle.isOn = false;
        }

        //Achievement notification
        if(PlayerPrefs.GetInt("video_achievementNotification") == 1) {
            achievementNotification.isOn = true;
        }
        else {
            achievementNotification.isOn = false;
        }

        //Advanced stats
        //Show framerates
        if(PlayerPrefs.GetString("video_ShowFramerates") == "true") {
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
        if(graphicsQualityDropdown.value == 0) {
            limitFPSDropdown.interactable = false;
            limitFPSDropdown.value = 0;
            limitFPSDropdown.RefreshShownValue();
            customLimitFPSObject.SetActive(false);
            customLimitFPSInputField.interactable = false;
        }
        //Advanced Graphics quality
        //Models Quality
        modelsQualityDropdown.value = PlayerPrefs.GetInt("video_GraphicsQuality_ModelsQuality");
        modelsQualityDropdown.RefreshShownValue();
        //Textures Quality
        texturesQualityDropdown.value = PlayerPrefs.GetInt("video_GraphicsQuality_TexturesQuality");
        texturesQualityDropdown.RefreshShownValue();
        //Shadows Quality
        shadowsQualityDropdown.value = PlayerPrefs.GetInt("video_GraphicsQuality_ShadowsQuality");
        shadowsQualityDropdown.RefreshShownValue();
        //Particules Quality
        particulesQualityDropdown.value = PlayerPrefs.GetInt("video_GraphicsQuality_ParticulesQuality");
        particulesQualityDropdown.RefreshShownValue();
        //Reflections Quality
        if(PlayerPrefs.GetInt("video_GraphicsQuality_ReflectionsQuality") == 1) {
            reflectionsQualityToggle.isOn = true;
        }
        else {
            reflectionsQualityToggle.isOn = false;
        }
        //Antialiasing Quality
        int antialiasingLevel = PlayerPrefs.GetInt("video_GraphicsQuality_Antialiasing");
        if(antialiasingLevel == 0) { //None
            AntialiasingQualityDropdown.value = 0;
        }
        else {
            if(antialiasingLevel == 2) { //x2
                AntialiasingQualityDropdown.value = 1;
            }
            else if(antialiasingLevel == 4) { //x4
                AntialiasingQualityDropdown.value = 2;
            }
            else if(antialiasingLevel == 8) { //x8
                AntialiasingQualityDropdown.value = 3;
            }
        }
        //VSync
        if(PlayerPrefs.GetInt("video_GraphicsQuality_VSync") == 1) {
            vsyncQualityToggle.isOn = true;
            limitFPSDropdown.interactable = false;
            limitFPSDropdown.value = 0;
            limitFPSDropdown.RefreshShownValue();
            customLimitFPSObject.SetActive(false);
            customLimitFPSInputField.interactable = false;
            limitFPSVSyncEnabledMessage.SetActive(true);
        }
        else {
            vsyncQualityToggle.isOn = false;
            limitFPSVSyncEnabledMessage.SetActive(false);
        }
        //Screenshot Quality
        int screenshotQuality = PlayerPrefs.GetInt("video_screenshotSuperSize");
        if (screenshotQuality == 0) { //SuperSize 0
            screenshotQualityDropdown.value = 0;
        }
        else {
            if (screenshotQuality == 2) { //SuperSize 2
                screenshotQualityDropdown.value = 1;
            }
            else if (screenshotQuality == 3) { //SuperSize 3
                screenshotQualityDropdown.value = 2;
            }
            else if (screenshotQuality == 5) { //SuperSize 5
                screenshotQualityDropdown.value = 3;
            }
            if (screenshotQuality == 7) { //SuperSize 7
                screenshotQualityDropdown.value = 4;
            }
        }

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
        if(limitFPSIndex != 3) {
            customLimitFPSObject.SetActive(false);

            if(limitFPSIndex == 0) {
                PlayerPrefs.SetInt("video_LimitFPS", -1);
            }
            if(limitFPSIndex == 1) {
                PlayerPrefs.SetInt("video_LimitFPS", 30);
            }
            if(limitFPSIndex == 2) {
                PlayerPrefs.SetInt("video_LimitFPS", 60);
            }
        }
        else {
            customLimitFPSInputField.text = PlayerPrefs.GetInt("video_CustomLimitFPS", PlayerPrefs.GetInt("video_LimitFPS")).ToString();
            customLimitFPSObject.SetActive(true);
        }
        if(limitFPSIndex != 0) {
            SetGraphicsQuality_VSync(false);
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
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality(true);
        if(graphicsQualityIndex != 0) { //Not 0 Low because it doesn't have VSync
            SetLimitFPS(0);
            limitFPSDropdown.interactable = false;
            limitFPSDropdown.value = 0;
            limitFPSDropdown.RefreshShownValue();
            customLimitFPSInputField.interactable = false;
            customLimitFPSObject.SetActive(false);
            limitFPSVSyncEnabledMessage.SetActive(true);
        }
        else {
            limitFPSDropdown.interactable = true;
            customLimitFPSInputField.interactable = true;
            limitFPSVSyncEnabledMessage.SetActive(false);
        }
    }
    //Advanced graphics quality
    #region
    //Models quality
    public void SetGraphicsQuality_ModelsQuality(int qualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality_ModelsQuality", qualityIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_ModelsQuality();
    }
    //Textures quality
    public void SetGraphicsQuality_TexturesQuality(int qualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality_TexturesQuality", qualityIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_TexturesQuality();
    }
    //Shadows quality
    public void SetGraphicsQuality_ShadowsQuality(int qualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality_ShadowsQuality", qualityIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_ShadowsQuality();
    }
    //Particules quality
    public void SetGraphicsQuality_ParticulesQuality(int qualityIndex) {
        PlayerPrefs.SetInt("video_GraphicsQuality_ParticulesQuality", qualityIndex);
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_ParticulesQuality();
    }
    //Reflections quality
    public void SetGraphicsQuality_ReflectionsQuality(bool use) {
        if(use) {
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 1);
        }
        else {
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 0);
        }
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_ReflectionsQuality();
    }
    //Antialiasing quality
    public void SetGraphicsQuality_Antialiasing(int qualityIndex) {
        if(qualityIndex == 0) { //None
            PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 0);
        }
        else {
            if (qualityIndex == 1) { //x2
                PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 2);
            }
            else if (qualityIndex == 2) { //x4
                PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 4);
            }
            else if (qualityIndex == 3) { //x8
                PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 8);
            }
        }
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_AntiAliasingQuality();
    }
    //VSync
    public void SetGraphicsQuality_VSync(bool use) {
        if(use) {
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 1);
            SetLimitFPS(0);
            limitFPSDropdown.interactable = false;
            limitFPSDropdown.value = 0;
            limitFPSDropdown.RefreshShownValue();
            customLimitFPSInputField.interactable = false;
            customLimitFPSObject.SetActive(false);
            limitFPSVSyncEnabledMessage.SetActive(true);
        }
        else {
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 0);
            limitFPSDropdown.interactable = true;
            customLimitFPSInputField.interactable = true;
            limitFPSVSyncEnabledMessage.SetActive(false);
        }
        GameCore_Main.instance.GetComponent<GameCore_Video>().SetGraphicsQuality_VSync();
    }
    #endregion
    //Screenshot quality
    public void SetScreenshotQuality(int qualityIndex) {
        if (qualityIndex == 0) { //SuperSize 0
            PlayerPrefs.SetInt("video_screenshotSuperSize", 0);
        }
        else if (qualityIndex == 1) { //SuperSize 2
            PlayerPrefs.SetInt("video_screenshotSuperSize", 2);
        }
        else if (qualityIndex == 2) { //SuperSize 3
            PlayerPrefs.SetInt("video_screenshotSuperSize", 3);
        }
        else if (qualityIndex == 3) { //SuperSize 5
            PlayerPrefs.SetInt("video_screenshotSuperSize", 5);
        }
        else if (qualityIndex == 4) { //SuperSize 7
            PlayerPrefs.SetInt("video_screenshotSuperSize", 7);
        }
    }
    #endregion

}
