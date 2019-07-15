using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

    [Header("Achievements utilities")]
    [SerializeField]
    private GameObject achievementNotification;
    [SerializeField]
    private Sprite[] achievementsLogos;
    public Sprite GetAchievementLogos(int index) {
        return achievementsLogos[index];
    }

    [Header("Achievements")]
    [SerializeField]
    private Achievement[] achievements;
    public Achievement[] GetAchievements() {
        return achievements;
    }

    public void LoadAchievements() {
        for (int i = 0; i < achievements.Length; i++) {
            Achievement currentAchievement = achievements[i];
            if(PlayerPrefs.GetString(currentAchievement.GetKey()) != "") {
                currentAchievement.SetCompletionDate(PlayerPrefs.GetString(currentAchievement.GetKey() + "_CompletionDate"));
                currentAchievement.SetCurrentPogress(PlayerPrefs.GetInt(currentAchievement.GetKey() + "_CurrentProgress"));
                currentAchievement.SetMaxProgress(PlayerPrefs.GetInt(currentAchievement.GetKey() + "_MaxProgress"));
                if(PlayerPrefs.GetInt(currentAchievement.GetKey() + "_Completed") == 1) {
                    currentAchievement.SetIsCompleted(true);
                }
                else {
                    currentAchievement.SetIsCompleted(false);
                }
            }
            //Update conditionFor list
            string[] conditions = currentAchievement.GetConditionnalAchievementsKey();
            int conditionLength = conditions.Length;
            if(conditionLength != 0) {
                for (int i2 = 0; i2 < conditionLength; i2++) {
                    int currentIndex = CheckIfExists(conditions[i2]);
                    achievements[currentIndex].AddConditionFor(currentAchievement.GetKey());
                    //Debug.Log("Set " + achievements[currentIndex].GetKey() + " / " + conditions[i2] + " as condition for " + currentAchievement.GetKey());
                }
            }
        }
    }

    private void SaveAchievement(int achievementIndex = -2, string achievementKey = "") {
        if(achievementIndex == -2) {
            if(achievementKey == "") {
                Debug.LogWarning("You need to use SaveAchievement with the index or the key of the achievement");
                return;
            }
            achievementIndex = CheckIfExists(achievementKey);
        }
        Achievement currentAchievement = achievements[achievementIndex];
        PlayerPrefs.SetString(currentAchievement.GetKey() + "_CompletionDate", currentAchievement.GetCompletionDate());
        if(currentAchievement.GetMaxProgress() != 0) {
            PlayerPrefs.SetInt(currentAchievement.GetKey() + "_CurrentProgress", currentAchievement.GetCurrentPogress());
            PlayerPrefs.SetInt(currentAchievement.GetKey() + "_MaxProgress", currentAchievement.GetMaxProgress());
        }
        if(currentAchievement.GetIsCompleted()) {
            PlayerPrefs.SetInt(currentAchievement.GetKey() + "_Completed", 1);
        }
        else {
            PlayerPrefs.SetInt(currentAchievement.GetKey() + "_Completed", 0);
        }
        PlayerPrefs.SetString(currentAchievement.GetKey(), currentAchievement.GetKey());
    }

    public void SetAchievementCompleted(string achievementKey) {
        int index = CheckIfExists(achievementKey);
        if(index != -1) {
            Achievement currentAchievement = achievements[index];
            int progress = currentAchievement.GetCurrentPogress();
            int maxProgress = currentAchievement.GetMaxProgress();
            if(progress == maxProgress) {
                if(!achievements[index].GetIsCompleted() && CheckCompletedConditionnalAchievements(currentAchievement.GetConditionnalAchievementsKey())) {
                    currentAchievement.SetCompletionDate(System.DateTime.Now.ToShortDateString());
                    currentAchievement.SetIsCompleted(true);
                    CreateAchievementNotification(index);
                    CheckIfCompletsConditionFor(currentAchievement.GetConditionFor());
                    //Debug.Log(achievementKey + " completed because completed and has now it's conditions completed too - ");
                }
                else {
                    return;
                }
            }
            else {
                progress += 1;
                currentAchievement.SetCurrentPogress(progress);
                if(progress == maxProgress && !currentAchievement.GetIsCompleted() && CheckCompletedConditionnalAchievements(currentAchievement.GetConditionnalAchievementsKey())) {
                    currentAchievement.SetCompletionDate(System.DateTime.Now.ToShortDateString());
                    currentAchievement.SetIsCompleted(true);
                    CreateAchievementNotification(index);
                    CheckIfCompletsConditionFor(currentAchievement.GetConditionFor());
                    //Debug.Log(achievementKey + " completed because completed and had it's conditions completed - ");
                }
            }
            SaveAchievement(index);
        }
    }

    private int CheckIfExists(string key) {
        for (int i = 0; i < achievements.Length; i++) {
            if(achievements[i].GetKey() == key) {
                return i;
            }
        }
        return -1;
    }

    private bool CheckCompletedConditionnalAchievements(string[] conditions) {
        int total = 0;
        for (int i = 0; i < conditions.Length; i++) {
            int conditionIndex = CheckIfExists(conditions[i]);
            if(conditionIndex != -1 && achievements[conditionIndex].GetIsCompleted()) {
                total += 1;
            }
        }
        if(total == conditions.Length) {
            return true;
        }
        return false;
    }

    private void CheckIfCompletsConditionFor(List<string> conditionFor) {
        int conditionForCount = conditionFor.Count;
        if(conditionForCount != 0) {
            for (int i = 0; i < conditionForCount; i++) {
                int currentIndex = CheckIfExists(conditionFor[i]);
                Achievement currentAchievement = achievements[currentIndex];
                if(!currentAchievement.GetIsCompleted() && currentAchievement.GetCurrentPogress() == currentAchievement.GetMaxProgress()) {
                    //Debug.Log("Check for " + achievements[currentIndex].GetKey() + " / " + conditionFor[i]);
                    SetAchievementCompleted(currentAchievement.GetKey());
                }
            }
        }
    }

    private void CreateAchievementNotification(int achievementIndex) {
        if(PlayerPrefs.GetInt("video_achievementNotification") == 1) {
            GameObject newAchievementNotification = (GameObject)Instantiate(achievementNotification, transform);
            newAchievementNotification.GetComponent<Notification>().SetNotification(achievements[achievementIndex].GetKey(), achievementsLogos[achievements[achievementIndex].GetLogoIndex()]);
        }
    }

}
