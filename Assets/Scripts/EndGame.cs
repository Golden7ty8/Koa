using UnityEngine;

public class EndGame : MonoBehaviour {

    [SerializeField]
    private bool playAudio = false;
    public AudioSource cameraAudioSource;
    public AudioClip endMusicClip;

    private bool active = true;

    [SerializeField]
    private GameObject completed;
    [SerializeField]
    private Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.tag == "Player") {
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
    }
}
