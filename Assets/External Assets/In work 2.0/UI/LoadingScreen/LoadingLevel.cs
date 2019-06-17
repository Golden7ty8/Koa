using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour {

    [Header("UIs")]
    public GameObject loadingLevelCanvas;
    public Image loadingBar;
    [Header("LevelBackrgounds")]
    public GameObject[] background_level;

    public void showLevelBackrgound(int levelNumber)
    {
        if(background_level[levelNumber] != null) {
            background_level[levelNumber].SetActive(true);
        }
        else  {
            background_level[0].SetActive(true);
            Debug.LogWarning("<color=red>The loading background for that scene does not exits or isn't referenced in the LoadingLevel object</color>");
        }
    }

    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {
        loadingLevelCanvas.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        //While loading
        while (!loadOperation.isDone) {

            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingBar.fillAmount = progress;

            yield return null;

        }
    }

}
