using UnityEngine;
using System.Collections;

public class GameCore_Video : MonoBehaviour {

    [SerializeField]
    private bool isReady = false;

    // ***x are the options not working or complete yet

    //Reset video options
    public void ResetVideoOptions(GameObject caller) {

        //Language -> English or system language
        SystemLanguage defLanguage = Application.systemLanguage;
        if(defLanguage == SystemLanguage.French) {
            PlayerPrefs.SetInt("video_Language", 1);
        }
        else {
            PlayerPrefs.SetInt("video_Language", 0);
        }
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

        PlayerPrefs.SetInt("video_ScreenshotQuality", 0);
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
            SetGraphicsQuality(true);
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
        //Screen.SetResolution(newWidth, newHeight, newFullScreenMode);

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
    public void SetGraphicsQuality(bool forceExpensiveChanges = false) {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality");
        //QualitySettings.SetQualityLevel(qualityLevel, forceExpensiveChanges);
        //Low
        if(qualityLevel == 0) {
            PlayerPrefs.SetInt("video_GraphicsQuality_ModelsQuality", 0); //Low
            PlayerPrefs.SetInt("video_GraphicsQuality_TexturesQuality", 0); //Low
            PlayerPrefs.SetInt("video_GraphicsQuality_ShadowsQuality", 1); //Low
            PlayerPrefs.SetInt("video_GraphicsQuality_ParticulesQuality", 0); //Low
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 0); //None
            PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 0); //None
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 0); //None
        }
        //Medium
        else if(qualityLevel == 1) {
            PlayerPrefs.SetInt("video_GraphicsQuality_ModelsQuality", 1); //Medium
            PlayerPrefs.SetInt("video_GraphicsQuality_TexturesQuality", 1); //Medium
            PlayerPrefs.SetInt("video_GraphicsQuality_ShadowsQuality", 2); //Medium
            PlayerPrefs.SetInt("video_GraphicsQuality_ParticulesQuality", 1); //Medium
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 1); //Yes
            PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 0); //None
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 1); //Yes
        }
        //High
        else if(qualityLevel == 2) {
            PlayerPrefs.SetInt("video_GraphicsQuality_ModelsQuality", 2); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_TexturesQuality", 2); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_ShadowsQuality", 3); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_ParticulesQuality", 2); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 1); //Yes
            PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 4); //x4
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 1); //Yes
        }
        //Ultra
        else if(qualityLevel == 3) {
            PlayerPrefs.SetInt("video_GraphicsQuality_ModelsQuality", 3); //Ultra
            PlayerPrefs.SetInt("video_GraphicsQuality_TexturesQuality", 2); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_ShadowsQuality", 4); //Ultra
            PlayerPrefs.SetInt("video_GraphicsQuality_ParticulesQuality", 2); //High
            PlayerPrefs.SetInt("video_GraphicsQuality_ReflectionsQuality", 1); //Yes
            PlayerPrefs.SetInt("video_GraphicsQuality_Antialiasing", 8); //x8
            PlayerPrefs.SetInt("video_GraphicsQuality_VSync", 1); //Yes
        }

        SetGraphicsQuality_ModelsQuality();
        SetGraphicsQuality_TexturesQuality();
        SetGraphicsQuality_ShadowsQuality();
        SetGraphicsQuality_ParticulesQuality();
        SetGraphicsQuality_ReflectionsQuality();
        SetGraphicsQuality_AntiAliasingQuality();
        SetGraphicsQuality_VSync();
    }

    public void SetGraphicsQuality_ModelsQuality() {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality_ModelsQuality");
        if (qualityLevel >= 1) { //Medium and higher
            if (qualityLevel == 1) { //Medium - LOD2
                QualitySettings.maximumLODLevel = 3;
            }
            else if (qualityLevel == 2) { //High - LOD1
                QualitySettings.maximumLODLevel = 2;
            }
            else if (qualityLevel == 3) { //Ultra - LOD0
                QualitySettings.maximumLODLevel = 1;
            }
        }
        else { //Low - LOD3
            QualitySettings.maximumLODLevel = 4;
        }
    }

    public void SetGraphicsQuality_TexturesQuality() {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality_TexturesQuality");
        if(qualityLevel >= 1) { //Medium and higher
            if (qualityLevel == 1) { //Medium
                QualitySettings.masterTextureLimit = 1; //Half
            }
            else if (qualityLevel == 2) { //High
                QualitySettings.masterTextureLimit = 0; //Full
            }
            else if (qualityLevel == 3) { //Ultra
            }
        }
        else { //Low
            QualitySettings.masterTextureLimit = 2; //Quarter
        }
    }

    public void SetGraphicsQuality_ShadowsQuality() {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality_ShadowsQuality");
        if (qualityLevel >= 2) { //Medium and higher
            QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask; //Realtime and backed shadows
            if (qualityLevel == 2) { //Medium
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowCascades = 2;
            }
            else if (qualityLevel == 3) { //High
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowCascades = 2;
            }
            else if (qualityLevel == 4) { //Ultra
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                QualitySettings.shadowDistance = 150;
                QualitySettings.shadowCascades = 4;
            }
        }
        else { //Low or none
            if (qualityLevel == 1) { //Low
                QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowCascades = 0;
            }
            else { //None
                QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask; //Backed shadows
                QualitySettings.shadows = ShadowQuality.Disable;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowDistance = 0;
                QualitySettings.shadowCascades = 0;
            }
        }
    }

    public void SetGraphicsQuality_ParticulesQuality() {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality_ParticulesQuality");
        
        if(qualityLevel >= 1) { //Medium and higher
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
            QualitySettings.softParticles = true;
            if(qualityLevel == 1) { //Medium
                QualitySettings.particleRaycastBudget = 64;
            }
            else if (qualityLevel == 2) { //High
                QualitySettings.particleRaycastBudget = 1024;
            }
            else if (qualityLevel == 3) { //Ultra
                QualitySettings.particleRaycastBudget = 4096;
            }
        }
        else { //Low
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.softParticles = false;
            QualitySettings.particleRaycastBudget = 16;
        }
    }

    public void SetGraphicsQuality_ReflectionsQuality() {
        int qualityLevel = PlayerPrefs.GetInt("video_GraphicsQuality_ReflectionsQuality");
        if (qualityLevel == 1) { //True
            QualitySettings.realtimeReflectionProbes = true;
        }
        else { //False
            QualitySettings.realtimeReflectionProbes = false;
        }
    }

    public void SetGraphicsQuality_AntiAliasingQuality() {
        QualitySettings.antiAliasing = PlayerPrefs.GetInt("video_GraphicsQuality_Antialiasing");
    }

    public void SetGraphicsQuality_VSync() {
        int use = PlayerPrefs.GetInt("video_GraphicsQuality_VSync");
        if(use == 1) { //True
            QualitySettings.vSyncCount = 1;
        }
        else { //False
            QualitySettings.vSyncCount = 0;
        }
    }
    #endregion

}
