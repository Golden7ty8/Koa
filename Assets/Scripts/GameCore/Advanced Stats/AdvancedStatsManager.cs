using UnityEngine;
using UnityEngine.UI;

public class AdvancedStatsManager : MonoBehaviour {

    //public static AdvancedStatsManager instance;

    //[SerializeField]
    //public bool isReady = false;

    [SerializeField]
    private GameObject showFrameratesObject;
    [SerializeField]
    private Text frameratesCounter;
    [SerializeField]
    private GameObject showSystemClockObject;
    [SerializeField]
    private Text systemClockCounter;

    /*public void Load() {
        if(AdvancedStatsManager.instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(AdvancedStatsManager.instance != null) {
            Destroy(gameObject);
        }
        UpdateAdvancedStatsUI();
        isReady = true;
    }

    public bool CheckIfReady() {
        if(isReady) {
            return true;
        }
        return false;
    }*/

    //Update advanced stats UI
    public void UpdateAdvancedStatsUI() {
        //List 1
        if(PlayerPrefs.GetString("video_ShowFramerates") == "true") {
            showFrameratesObject.SetActive(true);
        }
        else {
            showFrameratesObject.SetActive(false);
        }
        
        //List 2
        if(PlayerPrefs.GetString("video_ShowSystemClock") == "true") {
            showSystemClockObject.SetActive(true);
        }
        else {
            showSystemClockObject.SetActive(false);
        }

    }

}
