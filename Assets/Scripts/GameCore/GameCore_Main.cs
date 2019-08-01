using UnityEngine;

public class GameCore_Main : MonoBehaviour {

    public static GameCore_Main instance;

    private void Awake() {

        if(GameCore_Main.instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(GameCore_Main.instance != null) {
            Destroy(gameObject);
        }
        
    }

}
