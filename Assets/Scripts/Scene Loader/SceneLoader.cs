using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    [Header("UI")]
    [SerializeField]
    private GameObject loadingScene;
    [SerializeField]
    private Image progressBar;

    [Header("Backgrounds")]
    [SerializeField]
    private GameObject[] backgrounds;

    //SceneName
    public void LoadSceneFromName(string sceneName)  {
        StartCoroutine(LoadAsynchronouslyFromName(sceneName));
    }

    private IEnumerator LoadAsynchronouslyFromName(string sceneName) {
        loadingScene.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progress;
            yield return null;
        }

    }

    public void SetLoadingBackground(int sceneIndex) {

        if(backgrounds[sceneIndex] != null) {
            backgrounds[sceneIndex].SetActive(true);
        }
        else {
            backgrounds[0].SetActive(true);
            Debug.LogWarning("<color=red>No background set for that scene</color>");
        }

    }

    public string GetCurrentSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public void ExitTheGame() {

        Application.Quit();

    }

}
