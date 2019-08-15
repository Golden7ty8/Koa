using UnityEngine;

public class ResetPlayerPrefsOnStart : MonoBehaviour {
    public bool reset;
    void Awake() {
        if(reset)
            PlayerPrefs.DeleteAll();
    }
}
