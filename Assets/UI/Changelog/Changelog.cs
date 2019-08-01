using UnityEngine;
using UnityEngine.UI;

public class Changelog : MonoBehaviour {

    [Header("Main")]
    [SerializeField]
    private GameObject UpdateScreen;

    [Header("Update bar")]
    [SerializeField]
    private GameObject updateVersion;
    [SerializeField]
    private GameObject updateTitle;
    [SerializeField]
    private GameObject updateInfo;

    [Header("Changelog screen")]
    [SerializeField]
    private GameObject changelogScreen;
    [SerializeField]
    private GameObject changelogList;

    private void Start() {

        if(Application.version != PlayerPrefs.GetString("lastVersion") && Application.version != "1.0") {
            ShowUpdateBar();
            PlayerPrefs.SetString("lastVersion", Application.version);
        }
        
    }

    public void ShowUpdateBar() {

        SetUpdateBar();
        UpdateScreen.SetActive(true);

    }

    public void ShowChangelogScreen() {

        SetChangelogScreen();
        changelogScreen.SetActive(true);

    }

    private void SetUpdateBar() {
        if(LocalizationManager.instance.currentLanguage == "localizationText_en") {
            GetComponent<ChangelogManager>().LoadLocalizedText("changelog_en");
        }
        else if (LocalizationManager.instance.currentLanguage == "localizationText_fr") {
            GetComponent<ChangelogManager>().LoadLocalizedText("changelog_fr");
        }

        Debug.Log(Application.version);
        updateVersion.GetComponent<ChangelogText>().key = "Update_" + Application.version;
        updateTitle.GetComponent<ChangelogText>().key = "UpdateTitle_" + Application.version;
        updateInfo.GetComponent<ChangelogText>().key = "UpdateInfo_" + Application.version;
    }

    private void SetChangelogScreen() {
        if (LocalizationManager.instance.currentLanguage == "localizationText_en") {
            GetComponent<ChangelogManager>().LoadLocalizedText("changelog_en");
        }
        else if (LocalizationManager.instance.currentLanguage == "localizationText_fr") {
            GetComponent<ChangelogManager>().LoadLocalizedText("changelog_fr");
        }

        //changelogList.GetComponent<ChangelogText>().key = "changelog_" + Application.version;
        changelogList.GetComponent<ChangelogText>().key = "changelogContent";

    }

}
