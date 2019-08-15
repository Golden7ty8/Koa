using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private bool canCount = false;
    public bool GetCanCount() {
        return canCount;
    }
    private Text timerText;
    private float startTime;
    [Header("Timer (in seconds)")]
    [SerializeField]
    private float timerTime;
    [SerializeField]
    private Color endColor;

    private void Awake() {
        LoadTimer();
    }

    private void LoadTimer() {
        timerText = GetComponent<Text>();
        startTime = Time.time;
        canCount = true;
    }

    private void Update() {
        if(!canCount) {
            return;
        }
        timerTime -= Time.deltaTime;
        if(timerTime <= 0) {
            timerTime = 0;
            canCount = false;
            timerText.color = endColor;
        }
        timerText.text = timerTime.ToString("f0");
    }

    public void Win() {
        canCount = false;
        AdvancedGameUI.instance.GetAchievementManager().SetAchievementCompleted("winFast");
    }

}
