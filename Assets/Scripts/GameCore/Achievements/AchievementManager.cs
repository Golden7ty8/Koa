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
            string currentAchievementKey = currentAchievement.GetKey();
            if(PlayerPrefs.GetString(currentAchievementKey) != "") {
                currentAchievement.SetCompletionDate(PlayerPrefs.GetString(currentAchievementKey + "_CompletionDate"));
                currentAchievement.SetCurrentPogress(PlayerPrefs.GetInt(currentAchievementKey + "_CurrentProgress"));
                currentAchievement.SetMaxProgress(PlayerPrefs.GetInt(currentAchievementKey + "_MaxProgress"));
                if(PlayerPrefs.GetInt(currentAchievementKey + "_Completed") == 1) {
                    currentAchievement.SetIsCompleted(true);
                }
                else {
                    currentAchievement.SetIsCompleted(false);
                }

                if(PlayerPrefs.GetInt(currentAchievement.GetKey() + "_CanShow") == 1) {
                    currentAchievement.SetShowInMenu(true);
                }
                else {
                    currentAchievement.SetShowInMenu(false);
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
        string currentAchievementKey = currentAchievement.GetKey();
        PlayerPrefs.SetString(currentAchievementKey + "_CompletionDate", currentAchievement.GetCompletionDate());
        if(currentAchievement.GetMaxProgress() != 0) {
            PlayerPrefs.SetInt(currentAchievementKey + "_CurrentProgress", currentAchievement.GetCurrentPogress());
            PlayerPrefs.SetInt(currentAchievementKey + "_MaxProgress", currentAchievement.GetMaxProgress());
        }
        if(currentAchievement.GetIsCompleted()) {
            PlayerPrefs.SetInt(currentAchievementKey + "_Completed", 1);
            PlayerPrefs.SetInt(currentAchievementKey + "_CanShow", 1);
        }
        else {
            PlayerPrefs.SetInt(currentAchievementKey + "_Completed", 0);
            PlayerPrefs.SetInt(currentAchievementKey + "_CanShow", 0);
        }
        PlayerPrefs.SetString(currentAchievementKey, currentAchievement.GetKey());
    }

    public void ResetAchievements() {
        for (int i = 0; i < achievements.Length; i++) {
            Achievement currentAchievement = achievements[i];
            string currentAchievementKey = achievements[i].GetKey();
            PlayerPrefs.DeleteKey(currentAchievementKey);
            currentAchievement.SetCurrentPogress(0);
            PlayerPrefs.DeleteKey(currentAchievementKey + "_CurrentProgress");
            currentAchievement.SetCompletionDate("");
            PlayerPrefs.DeleteKey(currentAchievementKey + "_CompletionDate");
            currentAchievement.SetIsCompleted(false);
            PlayerPrefs.DeleteKey(currentAchievementKey + "_Completed");
            if(currentAchievement.GetShowInMenuBeforeCompleted()) {
                currentAchievement.SetShowInMenu(true);
                PlayerPrefs.SetInt(currentAchievementKey + "_CanShow", 1);
            }
            else {
                currentAchievement.SetShowInMenu(false);
                PlayerPrefs.SetInt(currentAchievementKey + "_CanShow", 0);
            }
        }
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
                    currentAchievement.SetShowInMenu(true);
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
                    currentAchievement.SetShowInMenu(true);
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
