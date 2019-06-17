using UnityEngine;
using System.Collections;

public class Changelog : MonoBehaviour {
    
    public string currentUpdate;

    public GameObject changelog;

    private void Awake() {
        //lastLoadUpdate = PlayerPrefs.GetString(lastLoadUpdate);
        if(PlayerPrefs.GetString("lastLoadedUpdate") != currentUpdate)
        {
            changelog.SetActive(true);
            PlayerPrefs.SetString("lastLoadedUpdate", currentUpdate);
        }
    }

}
