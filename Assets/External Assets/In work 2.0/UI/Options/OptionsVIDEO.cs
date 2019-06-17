using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionsVIDEO : MonoBehaviour {

    [Header("VIDEO")]
    public Dropdown languageDropdown;
    public Dropdown displayModeDropdown;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Dropdown targetFramerateDropdown;
    public GameObject customTargetFramerate;
    [Header("Advanced stats")]
    public Toggle framerateToggle;
    public Toggle systemClockToggle;
    [Header("Graphics Quality")]
    public Dropdown graphicsQualityDropdown;
    public Dropdown lightQualityDropdown;
    public Dropdown shadowQualityDropdown;

    public void Start() {
        //displayMode.captionText.GetComponent<LocalizedText>().key = "language";
        resolutions = Screen.resolutions;
        reloadVideoUI();
    }

    public void reloadVideoUI() {
        //Language
        if(PlayerPrefs.GetString("currentLanguage", "localizationText_en") == "localizationText_en") {
            PlayerPrefs.SetInt("currentLanguageIndex", 0);
        }
        else if (PlayerPrefs.GetString("currentLanguage", "localizationText_en") == "localizationText_fr") {
            PlayerPrefs.SetInt("currentLanguageIndex", 1);
        }
        languageDropdown.value = PlayerPrefs.GetInt("currentLanguageIndex");
        languageDropdown.RefreshShownValue();
        //Display Mode
        displayModeDropdown.value = PlayerPrefs.GetInt("currentDisplayMode");
        displayModeDropdown.RefreshShownValue();
        //Resolution
        video_resolution_start();
        //Framerate Target
        targetFramerateDropdown.value = PlayerPrefs.GetInt("framerateTargetIndex");
        //Stats
        video_stats_framerate_start();
        video_stats_systemClock_start();
        //Graphics Quality presset
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("currentGraphicQualityPreset");
        graphicsQualityDropdown.RefreshShownValue();
        //Graphics Quality
        lightQualityDropdown.value = PlayerPrefs.GetInt("currentLightQuality");
        lightQualityDropdown.RefreshShownValue();
        shadowQualityDropdown.value = PlayerPrefs.GetInt("currentShadowQuality");
        shadowQualityDropdown.RefreshShownValue();
    }

    public void resetVideoOptions() {
        GameCore_Video.gameCoreinstance.reset_VideoOptions();
        StartCoroutine(waitForVideoReset());
    }

    private IEnumerator waitForVideoReset() {
        while(GameCore_Video.gameCoreinstance.videoSet == false) {
            yield return new WaitForFixedUpdate();
        }
        reloadVideoUI();
        Debug.Log("<color=red>Video options UI has been reset</color>");
    }

    public void resetControlsOptions() {
        GameCore_Controls.gameCore_Controls_instance.resetControls();
    }

    //VIDEO
    //LANGUAGE
    public void video_Language(int languageIndex) {
        //English
        if (languageIndex == 0) {
            GameCore_Video.gameCoreinstance.set_language("localizationText_en");
        }
        //Français
        if (languageIndex == 1) {
            GameCore_Video.gameCoreinstance.set_language("localizationText_fr");
        }
        PlayerPrefs.SetInt("currentLanguageIndex", languageIndex);
    }
    //DISPLAY MODE
    public void video_displayMode(int displayModeIndex) {
        GameCore_Video.gameCoreinstance.set_displayMode(displayModeIndex);
    }
    //RESOLUTION
    public void video_resolution_start() {
        resolutionDropdown.ClearOptions();
        List<string> availableResolutions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string availabeResolution = resolutions[i].width + " x " + resolutions[i].height;
            availableResolutions.Add(availabeResolution);

            if(!PlayerPrefs.HasKey("currentResolutionIndex")) {
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    PlayerPrefs.SetInt("currentResolutionIndex", i);
                    Debug.Log(resolutions[i].width + " x " + resolutions[i].height);
                }
            }
        }
        resolutionDropdown.AddOptions(availableResolutions);
        resolutionDropdown.value = PlayerPrefs.GetInt("currentResolutionIndex");
        resolutionDropdown.RefreshShownValue();
    }
    public void video_resolution(int resolutionIndex) {
        Resolution choicedResolution = resolutions[resolutionIndex];
        GameCore_Video.gameCoreinstance.set_resolution(choicedResolution.width, choicedResolution.height);
        PlayerPrefs.SetInt("currentResolutionIndex", resolutionIndex);
    }
    //TARGET FRAMERATE
    public void video_targetFramerate(int targetFramerateIndex) {
        //Default
        if(targetFramerateIndex == 0) {
            PlayerPrefs.SetInt("framerateTarget", -1);
            PlayerPrefs.SetInt("framerateTargetIndex", 0);
            GameCore_Video.gameCoreinstance.set_targetFramerate(targetFramerateIndex);
            customTargetFramerate.SetActive(false);
        }
        //30
        if (targetFramerateIndex == 1) {
            PlayerPrefs.SetInt("framerateTarget", 30);
            PlayerPrefs.SetInt("framerateTargetIndex", 1);
            GameCore_Video.gameCoreinstance.set_targetFramerate(targetFramerateIndex);
            customTargetFramerate.SetActive(false);
        }
        //60
        if (targetFramerateIndex == 2) {
            PlayerPrefs.SetInt("framerateTarget", 60);
            PlayerPrefs.SetInt("framerateTargetIndex", 2);
            GameCore_Video.gameCoreinstance.set_targetFramerate(targetFramerateIndex);
            customTargetFramerate.SetActive(false);
        }
        //Custom
        if (targetFramerateIndex == 3) {
            customTargetFramerate.SetActive(true);
            PlayerPrefs.SetInt("framerateTargetIndex", 3);
            customTargetFramerate.GetComponentInChildren<InputField>().text = PlayerPrefs.GetInt("customFramerateTarget", 30).ToString();
        }
    }
        //CUSTOM TARGET FRAMERATE
        public void video_targetFramerateCustom(string customFramerate) {
            int.TryParse(customFramerate, out int newCustomFramerate);
            PlayerPrefs.SetInt("customFramerateTarget", newCustomFramerate);
            GameCore_Video.gameCoreinstance.set_targetFramerate(newCustomFramerate);
        }
    //ADVANCED STATS
    //FRAMERATE
    public void video_stats_framerate_start() {
        if (PlayerPrefs.GetString("statFramerate") == "True") {
            framerateToggle.isOn = true;
        }
        else {
            framerateToggle.isOn = false;
        }
    }
    public void video_stats_framerate(bool switchBool) {
        GameCore_Video.gameCoreinstance.set_stat_framerate(switchBool.ToString());
    }
    //SYSTEM CLOCK
    public void video_stats_systemClock_start() {
        if (PlayerPrefs.GetString("statSystemClock") == "True") {
            systemClockToggle.isOn = true;
        }
        else {
            systemClockToggle.isOn = false;
        }
    }
    public void video_stats_systemClock(bool switchBool) {
        GameCore_Video.gameCoreinstance.set_stat_systemClock(switchBool.ToString());
    }
    //GRAPHICS QUALITY
    public void video_graphicsQuality(int graphicsQualityIndex) {
        GameCore_Video.gameCoreinstance.set_graphicsQualityPreset(graphicsQualityIndex);
        lightQualityDropdown.value = PlayerPrefs.GetInt("currentLightQuality");
        lightQualityDropdown.RefreshShownValue();
        shadowQualityDropdown.value = PlayerPrefs.GetInt("currentShadowQuality");
        shadowQualityDropdown.RefreshShownValue();
    }
        //LIGHT QUALITY
        public void video_lightQuality2(int lightQualityIndex) {
            GameCore_Video.gameCoreinstance.set_gQ_light(lightQualityIndex);
        }
        //SHADOW QUALITY
        public void video_shadowQuality2(int shadowQualityIndex) {
            GameCore_Video.gameCoreinstance.set_gQ_shadow(shadowQualityIndex);
        }
    public void switchToCustomPreset() {
        PlayerPrefs.SetInt("currentGraphicQuality", 4);
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("currentGraphicQuality");
        graphicsQualityDropdown.RefreshShownValue();
    }
    //

}
