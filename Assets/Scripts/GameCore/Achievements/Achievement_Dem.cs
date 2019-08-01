using UnityEngine;

public class Achievement_Dem : MonoBehaviour {

    private AchievementManager achievementManager;

    private void Start() {

        achievementManager = AdvancedGameUI.instance.GetAchievementManager();
        
    }

    private void LateUpdate() {

        //LEARN KEYBOARD 1
        if(Input.GetKeyUp(KeyCode.Z)) {
            achievementManager.SetAchievementCompleted("Keyboard1");
        }
        //LEARN KEYBOARD 2
        if (Input.GetKeyUp(KeyCode.S)) {
            achievementManager.SetAchievementCompleted("Keyboard2");
        }
        //LEARN KEYBOARD 3
        //LEARN KEYBOARD 4
        if (Input.GetKeyUp(KeyCode.D)) {
            achievementManager.SetAchievementCompleted("Keyboard4");
        }

    }

}
