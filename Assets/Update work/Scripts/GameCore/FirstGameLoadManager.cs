using UnityEngine;
using System.Collections;

public class FirstGameLoadManager : MonoBehaviour {

    [SerializeField]
    private bool gameInitReady = false;
    [SerializeField]
    private int numberOfStates;
    [SerializeField]
    private GameObject sceneLoader;

    [Header("Game cores")]
    [SerializeField]
    private GameObject AdvancedStatsManagerObject;

    private void Start() {

        Debug.Log("<color=red>Start of the game Cores loading</color>");
        //Load the game in the order
        AdvancedStatsManagerObject.GetComponent<AdvancedStatsManager>().Load();

        StartCoroutine(LoadAdvancedStats());
        
    }

    private IEnumerator LoadAdvancedStats() {
        Debug.Log("A");

        yield return new WaitUntil(() => AdvancedStatsManager.instance.CheckIfReady() == true);

        Debug.Log("<color=red>- [1/" + numberOfStates + "] AdvancedStatsManager ready </color>");
        GameCore_Main.instance.GetComponent<GameCore_Video>().Load();
        StartCoroutine(LoadGameCores());
    }

    private IEnumerator LoadGameCores() {

        yield return new WaitUntil(() => GameCore_Main.instance.GetComponent<GameCore_Video>().CheckIfReady() == true);

        Debug.Log("<color=red>- [2/" + numberOfStates + "] GameCore_Main instance ready</color>");
        Debug.Log("<color=red>- [3/" + numberOfStates + "] GameCore_Video ready, video options should be applied</color>");
        GameCore_Main.instance.GetComponent<GameCore_Audio>().Load();
        yield return new WaitUntil(() => GameCore_Main.instance.GetComponent<GameCore_Audio>().CheckIfReady() == true);

        Debug.Log("<color=red>- [4/" + numberOfStates + "] GameCore_Audio ready, audio options should be applied</color>");
        GameCore_Main.instance.GetComponent<GameCore_Controls>().Load();
        yield return new WaitUntil(() => GameCore_Main.instance.GetComponent<GameCore_Controls>().CheckIfReady() == true);

        Debug.Log("<color=red>- [5/" + numberOfStates + "] GameCore_Controls ready, controls should be applied</color>");

        Debug.Log("<color=green>Game Cores loaded ! Load of the MainMenu</color>");
        gameInitReady = true;
        MainMenu();

    }

    private void MainMenu() {

        sceneLoader.GetComponent<SceneLoader>().SetLoadingBackground(0);
        sceneLoader.GetComponent<SceneLoader>().LoadSceneFromName("MainMenu");

    }

}
