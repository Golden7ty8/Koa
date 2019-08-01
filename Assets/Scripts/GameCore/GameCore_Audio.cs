using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class GameCore_Audio : MonoBehaviour {

    [SerializeField]
    private bool isReady = false;

    [SerializeField]
    private AudioMixer audioMixer;

    // ***x are the options not working or complete yet

    //Reset video options
    public void ResetAudioOptions(GameObject caller) {

        //Master volume -> 80 (80 - 100)
        PlayerPrefs.SetFloat("audio_MasterVolume", -20);
        PlayerPrefs.SetInt("audio_MasterVolume100", 80);
        SetMasterVolume();
        //Sound effect volume -> 100 (100 - 100)
        PlayerPrefs.SetFloat("audio_SoundEffectVolume", 0);
        PlayerPrefs.SetInt("audio_SoundEffectVolume100", 100);
        SetSoundEffectVolume();
        //Music volume -> 100 (100 - 100)
        PlayerPrefs.SetFloat("audio_MusicVolume", 0);
        Debug.Log("1. PlayerPrefs.GetFloat(audio_MusicVolume) = " + PlayerPrefs.GetFloat("audio_MusicVolume"));
        PlayerPrefs.SetInt("audio_MusicVolume100", 100);
        SetMusicVolume();
        //Subtitle -> No
        PlayerPrefs.SetString("audio_Subtitle", "false");
        SetSubtitle();
        //Subtitle language -> English
        PlayerPrefs.SetInt("audio_Subtitle_Language", 0);
        SetSubtitle_Language();

        Debug.Log("Audio options should have been reset and saved at their default state");
        if(caller != null) {
            caller.GetComponent<Options_Audio>().UpdateUI();
        }

    }

    public void Load() {
        //If the game has never applied a first time default audio options
        if(PlayerPrefs.GetString("AudioOptionsAppliedFirstStart") != "true") {
            Debug.Log("Applying default options for audio options");
            ResetAudioOptions(null);
            PlayerPrefs.SetString("AudioOptionsAppliedFirstStart", "true");
        }
        else {
            SetMasterVolume();
            SetSoundEffectVolume();
            SetMusicVolume();
            SetSubtitle();
            SetSubtitle_Language();
        }
        isReady = true;
    }

    public bool CheckIfReady() {
        if (isReady) {
            return true;
        }
        return false;
    }

    //***Master volume
    public void SetMasterVolume() {
        float currentMasterVolume = PlayerPrefs.GetFloat("audio_MasterVolume");
        audioMixer.SetFloat("MasterVolume", currentMasterVolume);
    }

    //***Sound effect volule
    public void SetSoundEffectVolume() {
        float currentSoundEffectVolume = PlayerPrefs.GetFloat("audio_SoundEffectVolume");
        audioMixer.SetFloat("SoundEffectVolume", currentSoundEffectVolume);
    }

    //***Music volume
    public void SetMusicVolume() {
        float currentMusicVolume = PlayerPrefs.GetFloat("audio_MusicVolume");
        //Debug.Log("2. PlayerPrefs.GetFloat(audio_MusicVolume) = " + currentMusicVolume);
        audioMixer.SetFloat("MusicVolume", currentMusicVolume);
    }

    //***Subtitle
    public void SetSubtitle() {

    }

    //***Subtitle language
    public void SetSubtitle_Language() {

        //English
        if(PlayerPrefs.GetInt("audio_Subtitle_Language") == 0) {

        }
        //Français
        if(PlayerPrefs.GetInt("audio_Subtitle_Language") == 1) {

        }

    }

}
