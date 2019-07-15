using UnityEngine;
using UnityEngine.UI;

public class AchievementUIManager : MonoBehaviour {

    [SerializeField]
    private AchievementUI achievementUI;
    [SerializeField]
    private Transform parent;
    private AchievementManager achievementManager;

    public void UpdateAchievementUI() {
        achievementManager = AdvancedGameUI.instance.GetAchievementManager();
        Achievement[] achievements = achievementManager.GetAchievements();
        for (int i = 0; i < achievements.Length; i++) {
            Achievement currentAchievement = achievements[i];
            AchievementUI newAchievementUI = (AchievementUI)Instantiate(achievementUI, parent);
            newAchievementUI.transform.localScale = Vector3.one;
            if (currentAchievement.GetShowInMenu()) {
                newAchievementUI.SetAchievementUI(currentAchievement.GetName(), currentAchievement.GetDescription(), currentAchievement.GetLogoIndex(), currentAchievement.GetCompletionDate());
            }
            else {
                newAchievementUI.SetAchievementUI("achievement_Hidden_Title", "achievement_Hidden_Description", currentAchievement.GetLogoIndex(), "");
            }
        }
    }

    public void Clear() {
        for (int i = 0; i < parent.childCount; i++) {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

}
