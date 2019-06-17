using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
