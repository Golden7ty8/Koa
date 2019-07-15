using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Achievement {

    [SerializeField]
    private string key;
    [SerializeField]
    private string name;
    [SerializeField]
    private string descritption;
    [SerializeField]
    private int logoIndex;
    [SerializeField]
    private int maxProgress = 1;
    [SerializeField]
    private int currentProgress;
    [SerializeField]
    private bool isCompleted;
    [SerializeField]
    private string completionDate;
    [SerializeField]
    private bool showInMenu;
    [SerializeField]
    private string[] conditionnalAchievementsKey;
    //[SerializeField]
    private List<string> conditionFor = new List<string>();

    public string GetKey() {
        return key;
    }
    public string GetName() {
        return name;
    }
    public string GetDescription() {
        return descritption;
    }
    public int GetLogoIndex() {
        return logoIndex;
    }
    public int GetMaxProgress() {
        return maxProgress;
    }
    public int GetCurrentPogress() {
        return currentProgress;
    }
    public bool GetIsCompleted() {
        return isCompleted;
    }
    public string GetCompletionDate() {
        return completionDate;
    }
    public bool GetShowInMenu() {
        return showInMenu;
    }
    public string[] GetConditionnalAchievementsKey() {
        return conditionnalAchievementsKey;
    }
    public List<string> GetConditionFor() {
        return conditionFor;
    }

    public void SetMaxProgress(int newMaxProgress) {
        maxProgress = newMaxProgress;
    }
    public void SetCurrentPogress(int newCurrentProgress) {
        currentProgress = newCurrentProgress;
    }
    public void SetIsCompleted(bool newIsCompleted) {
        isCompleted = newIsCompleted;
    }
    public void SetCompletionDate(string newCompletionDate) {
        completionDate = newCompletionDate;
    }
    public void SetShowInMenu(bool newShowInMenu) {
        showInMenu = newShowInMenu;
    }
    public void AddConditionFor(string newConditionForKey) {
        conditionFor.Add(newConditionForKey);
    }

}
