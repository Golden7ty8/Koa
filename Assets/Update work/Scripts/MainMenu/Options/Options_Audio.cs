using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Options_Audio : MonoBehaviour {

    [Header("Audio options")]
    //Master volume
    [SerializeField]
    private Slider masterVolumeSlider;
    [SerializeField]
    private Text masterVolumeCounter;

    [Header("Specific audio options")]
    [SerializeField]
    private Slider soundEffectVolumeSlider;
    [SerializeField]
    private Text soundEffectVolumeCounter;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Text musicVolumeCounter;

    [Header("Subtitles")]
    [SerializeField]
    private Toggle subtitlesToggle;
    [SerializeField]
    private GameObject subtitleOptionsArea;
    [SerializeField]
    private Dropdown subtitlesLanguageDropdown;

    private void Start() {
        UpdateUI();
    }

    public void CallResetAudioOptions() {

        GameCore_Main.instance.GetComponent<GameCore_Audio>().ResetAudioOptions(this.gameObject);
        //UpdateUI();

    }

    public void UpdateUI() {

        //Master volume
        masterVolumeSlider.value = PlayerPrefs.GetInt("audio_MasterVolume100");
        masterVolumeCounter.text = masterVolumeSlider.value.ToString();

        //Sound effect volume
        soundEffectVolumeSlider.value = PlayerPrefs.GetInt("audio_SoundEffectVolume100");
        soundEffectVolumeCounter.text = soundEffectVolumeSlider.value.ToString();

        //Music volume
        musicVolumeSlider.value = PlayerPrefs.GetInt("audio_MusicVolume100");
        musicVolumeCounter.text = musicVolumeSlider.value.ToString();

        //Subtitle
        if(PlayerPrefs.GetString("audio_Subtitle") == "true") {
            subtitlesToggle.isOn = true;
            subtitleOptionsArea.SetActive(true);
            subtitlesLanguageDropdown.value = PlayerPrefs.GetInt("audio_Subtitle_Language");
        }
        else {
            subtitlesToggle.isOn = false;
            subtitleOptionsArea.SetActive(false);
        }

    }

    //Options
    #region
        //Master volume
    public void SetMasterVolume(float masterVolume) {

        PlayerPrefs.SetInt("audio_MasterVolume100", (int)masterVolume);
        masterVolume = (100 - masterVolume) * 0.8f;
        PlayerPrefs.SetFloat("audio_MasterVolume", -masterVolume);
        GameCore_Main.instance.GetComponent<GameCore_Audio>().SetMasterVolume();

    }

    //Sound effect volume
    public void SetSoundEffectVolume(float soundEffectVolume) {

        PlayerPrefs.SetInt("audio_SoundEffectVolume100", (int)soundEffectVolume);
        soundEffectVolume = (100 - soundEffectVolume) * 0.8f;
        PlayerPrefs.SetFloat("audio_SoundEffectVolume", -soundEffectVolume);
        GameCore_Main.instance.GetComponent<GameCore_Audio>().SetSoundEffectVolume();

    }

    //Music volume
    public void SetMusicVolume(float musicVolume) {

        PlayerPrefs.SetInt("audio_MusicVolume100", (int)musicVolume);
        musicVolume = (100 - musicVolume) * 0.8f;
        PlayerPrefs.SetFloat("audio_MusicVolume", -musicVolume);
        GameCore_Main.instance.GetComponent<GameCore_Audio>().SetMusicVolume();

    }

    //Subtitle
    public void SetSubtitle(bool subtitle) {

        if(subtitle) {
            PlayerPrefs.SetString("audio_Subtitle", "true");
            subtitleOptionsArea.SetActive(true);
        }
        else if (!subtitle) {
            PlayerPrefs.SetString("audio_Subtitle", "false");
            subtitleOptionsArea.SetActive(false);
        }
        GameCore_Main.instance.GetComponent<GameCore_Audio>().SetSubtitle();

    }

    //Subtitle language
    public void SetSubtitleLanguage(int languageIndex) {

        PlayerPrefs.SetInt("audio_Subtitle_Language", languageIndex);
        GameCore_Main.instance.GetComponent<GameCore_Audio>().SetSubtitle_Language();

    }

    #endregion

}
