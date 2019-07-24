using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    public ControllerPlayer controllerPlayer;
    public float speedMult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            controllerPlayer.walkSpeed *= speedMult;

            //Mark the acheivment as completed.
            AchievementManager achievementManager = AdvancedGameUI.instance.GetAchievementManager();
            achievementManager.SetAchievementCompleted("shortcut");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            controllerPlayer.walkSpeed /= speedMult;
        }
    }

}
