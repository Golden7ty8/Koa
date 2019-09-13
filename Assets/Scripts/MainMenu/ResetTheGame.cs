using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetTheGame : MonoBehaviour {

    public void Reset() {

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LoadMainMenu", LoadSceneMode.Single);

    }

}
