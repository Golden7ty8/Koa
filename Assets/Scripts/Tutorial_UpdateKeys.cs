using UnityEngine;

public class Tutorial_UpdateKeys : MonoBehaviour {

    [SerializeField]
    private Tutorial_GetControls[] tutorialControls;

    private KeyCode pauseMenuKey;

    private void Start() {
        pauseMenuKey = AdvancedGameUI.instance.GetPauseMenu().GetPauseMenyKey();
    }

    private void LateUpdate() {
        if(Input.GetKeyUp(pauseMenuKey) && !AdvancedGameUI.instance.GetPauseMenu().GetPauseMenuEnabled()) {
            for (int i = 0; i < tutorialControls.Length; i++) {
                tutorialControls[i].Reload();
            }
        }
    }

}
