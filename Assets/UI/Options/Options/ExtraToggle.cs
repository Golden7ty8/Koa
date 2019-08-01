using UnityEngine;
using UnityEngine.UI;

public class ExtraToggle : MonoBehaviour {

    public bool isOn = false;
    public Image Open;
    public Image Close;
    public GameObject ExtraArea;

    public void Toggle() {

        isOn = !isOn;
        if(isOn) {
            GetComponent<Button>().targetGraphic = Open;
            Close.gameObject.SetActive(false);
            Open.gameObject.SetActive(true);
            ExtraArea.SetActive(true);
        }
        else if(!isOn) {
            GetComponent<Button>().targetGraphic = Close;
            Close.gameObject.SetActive(true);
            Open.gameObject.SetActive(false);
            ExtraArea.SetActive(false);
        }

    }

}
