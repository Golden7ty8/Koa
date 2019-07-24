using UnityEngine;

public class AdvancedGameUI : MonoBehaviour {

    public static AdvancedGameUI instance;

    [SerializeField]
    public bool isReady = false;
    public bool CheckIfReady() {
        return isReady;
    }

    [Header("Advanced UIs")]
    //Advanced stats
    [SerializeField]
    private GameObject advancedStats; //Object
    public GameObject GetAdvancedStats_Object() {
        return advancedStats;
    }
    private AdvancedStatsManager advancedStatsManager;
    public AdvancedStatsManager GetAdvancedStatsManager() {
        advancedStatsManager = advancedStats.GetComponent<AdvancedStatsManager>();
        return advancedStatsManager;
    }
    //Achievements
    [SerializeField]
    private GameObject achievements; //Object
    public GameObject GetAchievement() {
        return achievements;
    }
    private AchievementManager achievementManager;
    public AchievementManager GetAchievementManager() {
        achievementManager = achievements.GetComponent<AchievementManager>();
        return achievementManager;
    }

    public void Load() {
        if(AdvancedGameUI.instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(AdvancedGameUI.instance != null) {
            Destroy(gameObject);
        }
        isReady = true;

        GetAchievementManager().LoadAchievements();
    }

}
