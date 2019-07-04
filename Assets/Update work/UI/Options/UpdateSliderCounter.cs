using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderCounter : MonoBehaviour {

    private Text counter;

    private void Start() {
        counter = GetComponent<Text>();
    }

    public void UpdateCounter(float newValue) {
        if(counter == null) {
            return;
        }
        counter.text = newValue.ToString();
    }

    public void UpdateCounterPercentage(float newValue) {
        counter.text = newValue.ToString() + " %";
    }

}
