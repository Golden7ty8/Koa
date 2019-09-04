using UnityEngine;

public class ResetTheGame : MonoBehaviour {

    public void Reset() {

        GameCore_Main.instance.GetComponent<GameCore_Video>().ResetVideoOptions(null);
        GameCore_Main.instance.GetComponent<GameCore_Audio>().ResetAudioOptions(null);
        GameCore_Main.instance.GetComponent<GameCore_Controls>().ResetControls(null);
        AdvancedGameUI.instance.GetAchievementManager().ResetAchievements();

    }

}
