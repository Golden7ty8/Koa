using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public ControllerPlayer controllerPlayer;

    public float creditsTime;
    public float fadeToWhiteTime;
    public GameObject whiteUIPanel;

    bool win = false;
    float winTimer;
    bool fadingToWhite = false;

    [SerializeField]
    private bool playAudio = false;
    public AudioSource cameraAudioSource;
    public AudioClip endMusicClip;

    private bool active = true;

    [SerializeField]
    private GameObject completed;
    [SerializeField]
    private Timer timer;

    private void Start() {
        completed.SetActive(false);
        winTimer = creditsTime;
    }

    private void LateUpdate() {
        if(win) {
            WinTransition();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(active && other.tag == "Player") {
            active = false;
            CheckTimer();
            EndGameFunc();
        }
    }

    private void CheckTimer() {
        if(timer.GetCanCount()) {
            timer.Win();
        }
    }

    void EndGameFunc() {
        if(playAudio) {
            cameraAudioSource.clip = endMusicClip;
            cameraAudioSource.Play();
        }
        completed.SetActive(true);
        AdvancedGameUI.instance.GetAchievementManager().SetAchievementCompleted("win");
        controllerPlayer.GetComponent<AudioSource>().PlayOneShot(controllerPlayer.winSound.clip, controllerPlayer.winSound.volume);
        win = true;
    }

    private void WinTransition() {
        if (fadingToWhite) {
            Color tmp = whiteUIPanel.GetComponent<Image>().color;
            whiteUIPanel.GetComponent<Image>().color = new Color(tmp.r, tmp.g, tmp.b, 1.0f - (winTimer / fadeToWhiteTime));

            winTimer -= Time.deltaTime;

            if (winTimer <= 0) {
                winTimer = creditsTime;
                //Change to Credits Scene HERE!!!!!
                SceneManager.LoadScene("Credits", LoadSceneMode.Single);
            }
        }
        else {
            winTimer -= Time.deltaTime;

            if (winTimer <= 0) {
                fadingToWhite = true;
                winTimer = fadeToWhiteTime;
            }
        }
    }
}
