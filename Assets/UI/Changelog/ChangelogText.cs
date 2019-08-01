using UnityEngine;
using UnityEngine.UI;

public class ChangelogText : MonoBehaviour {

    public string key;
    [SerializeField]
    private ChangelogManager changelogMain;

    // Use this for initialization
    void Update() {
        Text text = GetComponent<Text>();
        text.text = changelogMain.GetLocalizedValue(key);
    }

}