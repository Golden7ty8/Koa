using UnityEngine;

public class Tutorial_UpdateKeys : MonoBehaviour {

    public static Tutorial_UpdateKeys instance;

    [SerializeField]
    private Tutorial_GetControls[] tutorialControls;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Script instance already existing");
        }
    }

    public void ReloadTutorialText() {
        for (int i = 0; i < tutorialControls.Length; i++) {
            tutorialControls[i].Reload();
        }
    }

}
