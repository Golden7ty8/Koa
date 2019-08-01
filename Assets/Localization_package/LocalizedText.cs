using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {

    public string key;
    private LocalizationManager instance;

    // Use this for initialization
    void Update() {
        instance = LocalizationManager.instance;
        Text text = GetComponent<Text>();
        text.text = instance.GetLocalizedValue(key);
    }

}