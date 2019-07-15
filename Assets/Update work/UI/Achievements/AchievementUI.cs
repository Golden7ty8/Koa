using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

    [SerializeField]
    private LocalizedText titleText;
    [SerializeField]
    private LocalizedText descriptionText;
    [SerializeField]
    private Image logoImage;
    [SerializeField]
    private Text completionDate;

    public void SetAchievementUI(string titleKey, string descriptionKey, int logoIndex, string CompletionDate) {
        titleText.key = titleKey;
        descriptionText.key = descriptionKey;
        logoImage.sprite = AdvancedGameUI.instance.GetAchievementManager().GetAchievementLogos(logoIndex);
        completionDate.text = CompletionDate;
    }

}
