using UnityEngine;
using System.Collections;

public class GameCore_Video : MonoBehaviour {

    public static GameCore_Video gameCoreinstance;

    [Header("Options - Video")]
    public GameObject framerateStat;
    public GameObject systemClockStat;
    public GameObject statsList1;
    public GameObject statsList2;
    public bool videoSet;
    [Header("Options - Audio")]
    public int masterVolume;

    private void Awake() {
        if(gameCoreinstance == null) {
            gameCoreinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameCoreinstance != null) {
            Destroy(gameObject);
        }

        Debug.Log("<color=green><b>Load of video options...</b></color>");
        reloadVideoOptions();
    }

    public void reloadVideoOptions() {
        //Language
        set_language(PlayerPrefs.GetString("currentLanguage", "localizationText_en"));
        //Display Mode
        set_displayMode(PlayerPrefs.GetInt("currentDisplayMode", 0));

        //Resolutions
        set_resolution(PlayerPrefs.GetInt("currentResolutionWidth", Screen.width), PlayerPrefs.GetInt("currentResolutionHeight", Screen.height));

        //Target framerate
        set_targetFramerate(PlayerPrefs.GetInt("framerateTargetIndex", 0));

        //Advanced stats
        StartCoroutine(waitForStatsInstance());
            //Framerate
        //set_stat_framerate(PlayerPrefs.GetString("statFramerate"));
            //System clock
        //set_stat_systemClock(PlayerPrefs.GetString("statSystemClock"));

        //Graphics quality
        set_graphicsQualityPreset(PlayerPrefs.GetInt("currentGraphicQualityPreset", 1));

        Debug.Log("<color=green><b>Video options sucessfully loaded !</b></color>");
        videoSet = true;
    }

    public void reloadAudioOptions() {
        //Master Volume
        set_masterVolume(PlayerPrefs.GetInt("currentMasterVolume", 100));
    }

    //Video
    #region
    public void reset_VideoOptions() {
        videoSet = false;
        PlayerPrefs.DeleteKey("currentDisplayMode");
        PlayerPrefs.DeleteKey("currentResolutionIndex");
        PlayerPrefs.DeleteKey("currentResolutionWidth");
        PlayerPrefs.DeleteKey("currentResolutionHeight");
        PlayerPrefs.DeleteKey("framerateTargetIndex");
        PlayerPrefs.DeleteKey("framerateTarget");
        PlayerPrefs.DeleteKey("customFramerateTarget");
        PlayerPrefs.DeleteKey("statFramerate");
        PlayerPrefs.DeleteKey("statSystemClock");
        PlayerPrefs.DeleteKey("currentGraphicQualityPreset");
        PlayerPrefs.DeleteKey("currentLightQuality");
        PlayerPrefs.DeleteKey("currentShadowQuality");
        Debug.Log("- Reset successful");
        reloadVideoOptions();
    }

    public void set_language(string languageName) {
        StartCoroutine(waitForLanguageInstance(languageName));
    }

    private IEnumerator waitForLanguageInstance(string languageName) {
        while(LocalizationManager.instance == null) {
            yield return new WaitForFixedUpdate();
        }
        LocalizationManager.instance.LoadLocalizedText(languageName);
        PlayerPrefs.SetString("currentLanguage", languageName);
        Debug.Log("- Language set as " + PlayerPrefs.GetString("currentLanguage"));
    }

    public void set_displayMode(int displayModeKind) {
        //Fullscreen
        if (displayModeKind == 0) {
            Screen.fullScreen = true;
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Debug.Log("- Display mode set as Fullscreen");
        }
        //Window
        if (displayModeKind == 1) {
            Screen.fullScreen = true;
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            Debug.Log("- Display mode set as Window");
        }
        //Window
        if (displayModeKind == 2) {
            Screen.fullScreen = true;
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Debug.Log("- Display mode set as Window");
        }
        PlayerPrefs.SetInt("currentDisplayMode", displayModeKind);
    }

    public void set_resolution(int resolutionWidth, int resolutionHeight) {
        PlayerPrefs.SetInt("currentResolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("currentResolutionHeight", resolutionHeight);
        Debug.Log("- Resolution set as " + PlayerPrefs.GetInt("currentResolutionWidth") + "x" + PlayerPrefs.GetInt("currentResolutionHeight"));
    }

    public void set_targetFramerate(int framerateTargetIndex) {
        if (framerateTargetIndex < 3) {
            Application.targetFrameRate = PlayerPrefs.GetInt("framerateTarget", -1);
            Debug.Log("- Use preset framerate traget");
            Debug.Log("- Target framerate set as " + PlayerPrefs.GetInt("framerateTarget", -1));
        }
        else {
            Application.targetFrameRate = PlayerPrefs.GetInt("customFramerateTarget");
            Debug.Log("- Use custom framerate target");
            Debug.Log("- Target framerate set as " + PlayerPrefs.GetInt("customFramerateTarget"));
        }
    }

    private IEnumerator waitForStatsInstance() {
        while (AdvancedStats.advancedStatsinstance == null) {
            yield return new WaitForFixedUpdate();
        }
        //Framerate
        set_stat_framerate(PlayerPrefs.GetString("statFramerate"));
        //System clock
        set_stat_systemClock(PlayerPrefs.GetString("statSystemClock"));
    }

    public void set_stat_framerate(string stat_Framerate) {
        if (stat_Framerate == "True") {
            AdvancedStats.advancedStatsinstance.Framerate.SetActive(true);
            Debug.Log("- Show advanced stat framerate");
        }
        else {
            AdvancedStats.advancedStatsinstance.Framerate.SetActive(false);
            Debug.Log("- Don't show advanced stat framerate");
        }
        PlayerPrefs.SetString("statFramerate", stat_Framerate);
        reload_statDisplay();
    }

    public void set_stat_systemClock(string stat_systemClock) {
        if (stat_systemClock == "True") {
            AdvancedStats.advancedStatsinstance.SystemClock.SetActive(true);
            Debug.Log("- Show advanced stat system clock");
        }
        else {
            AdvancedStats.advancedStatsinstance.SystemClock.SetActive(false);
            Debug.Log("- Don't show advanced stat system clock");
        }
        PlayerPrefs.SetString("statSystemClock", stat_systemClock);
        reload_statDisplay();
    }

    public void reload_statDisplay() {
        //Stat List 1
        if (!AdvancedStats.advancedStatsinstance.Framerate.activeSelf) {
            AdvancedStats.advancedStatsinstance.List1.SetActive(false);
        }
        else {
            AdvancedStats.advancedStatsinstance.List1.SetActive(true);
        }
        //Stat List 2
        if (!AdvancedStats.advancedStatsinstance.SystemClock.activeSelf) {
            AdvancedStats.advancedStatsinstance.List2.SetActive(false);
        }
        else {
            AdvancedStats.advancedStatsinstance.List2.SetActive(true);
        }
    }

    public void set_graphicsQualityPreset(int qualityPreset) {
        PlayerPrefs.SetInt("currentGraphicQualityPreset", qualityPreset);
        //Low
        if (qualityPreset == 0) {
            set_gQ_light(0);
            set_gQ_shadow(0);
            Debug.Log("- Graphics Quality Preset set as Low");
        }
        //Medium
        if (qualityPreset == 1) {
            set_gQ_light(1);
            set_gQ_shadow(1);
            Debug.Log("- Graphics Quality Preset set as Medium");
        }
        //High
        if (qualityPreset == 2) {
            set_gQ_light(2);
            set_gQ_shadow(2);
            Debug.Log("- Graphics Quality Preset set as High");
        }
        //High
        if (qualityPreset == 3) {
            set_gQ_light(3);
            set_gQ_shadow(3);
            Debug.Log("- Graphics Quality Preset set as Ultra");
        }
        //Custom
        if (qualityPreset == 4) {
            set_gQ_light(PlayerPrefs.GetInt("customLightQuality", 1));
            set_gQ_shadow(PlayerPrefs.GetInt("customShadowQuality", 1));
            Debug.Log("- Graphics Quality Preset set as Custom");
        }
    }

    public void set_gQ_light(int lightQuality) {
        PlayerPrefs.SetInt("currentLightQuality", lightQuality);
        if(PlayerPrefs.GetInt("currentGraphicQualityPreset", 1) == 4) {
            PlayerPrefs.SetInt("customLightQuality", lightQuality);
            Debug.Log("- Graphics Quality : Lights custom saved");
        }
        Debug.Log("- Graphics Quality : Lights set at " + lightQuality);
    }

    public void set_gQ_shadow(int shadowQuality) {
        PlayerPrefs.SetInt("currentShadowQuality", shadowQuality);
        if (PlayerPrefs.GetInt("currentGraphicQualityPreset", 1) == 4) {
            PlayerPrefs.SetInt("customShadowQuality", shadowQuality);
            Debug.Log("- Graphics Quality : Shadows custom saved");
        }
        Debug.Log("- Graphics Quality : Shadows set at " + shadowQuality);
    }
    #endregion

    //Audio
    #region
    public void set_masterVolume(int masterVolume) {

    }

    #endregion
}
