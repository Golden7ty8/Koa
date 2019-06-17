using UnityEngine;
using System.Collections;

public class AdvancedStats : MonoBehaviour {

    public static AdvancedStats advancedStatsinstance;

    public GameObject MainStats;
    [Header("Stats Lists 1")]
    public GameObject List1;
    public GameObject Framerate;
    [Header("Stats List 2")]
    public GameObject List2;
    public GameObject SystemClock;

    private void Start() {
        if(advancedStatsinstance == null) {
            advancedStatsinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (advancedStatsinstance != null) {
            Destroy(gameObject);
        }
    }

    public void HideStats() {
        MainStats.SetActive(false);
    }

    public void ShowStats() {
        MainStats.SetActive(true);
    }

}
