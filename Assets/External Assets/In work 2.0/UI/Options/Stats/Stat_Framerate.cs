using UnityEngine;
using UnityEngine.UI;

public class Stat_Framerate : MonoBehaviour {

    public Text framerateCountStat;
    //private int frameRate;
    private float currentFPS = 0;

    private void Update() {
        currentFPS += (Time.unscaledDeltaTime - currentFPS) * 0.1f;
        FPS();
    }

    private void FPS() {
        float msec = currentFPS * 1000.0f;
        float fps = 1.0f / currentFPS;
        framerateCountStat.text = string.Format("{0:0}", fps);
    }

    /*currentFPS = Time.frameCount / Time.time;
    frameRate = (int)currentFPS;
    framerateCountStat.text = frameRate.ToString();*/

}
