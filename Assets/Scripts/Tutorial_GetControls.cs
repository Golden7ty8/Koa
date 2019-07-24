using UnityEngine;
using UnityEngine.UI;

public class Tutorial_GetControls : MonoBehaviour {

    [SerializeField]
    private bool getMovementControls;
    [SerializeField]
    private bool getJumpControl;
    [SerializeField]
    private bool getRunControl;
    [SerializeField]
    private bool getBlackLightControl;

    private void Start() {
        if(getMovementControls) {
            //GetComponent<Text>().text = PlayerPrefs.GetString("walk_forward") + PlayerPrefs.GetString("walk_back") + PlayerPrefs.GetString("walk_left") + PlayerPrefs.GetString("walk_right");
            GetComponent<Text>().text = PlayerPrefs.GetString("walk_left") + PlayerPrefs.GetString("walk_right");
            return;
        }
        else if(getJumpControl) {
            GetComponent<Text>().text = PlayerPrefs.GetString("jump");
            return;
        }
        else if(getRunControl) {
            GetComponent<Text>().text = PlayerPrefs.GetString("run");
            return;
        }
        else if(getBlackLightControl) {
            GetComponent<Text>().text = PlayerPrefs.GetString("black_light");
            return;
        }
    }

}
