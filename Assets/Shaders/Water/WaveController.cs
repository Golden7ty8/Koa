using UnityEngine;

public class WaveController : MonoBehaviour {

    [SerializeField]
    private float wavesHeight;
    [SerializeField]
    private float cycleTime;

    private void Start() {
        iTween.MoveBy(gameObject, iTween.Hash("y", wavesHeight, "time", cycleTime, "looptype", "pingpong", "easetype", iTween.EaseType.easeInOutSine));
    }

}
