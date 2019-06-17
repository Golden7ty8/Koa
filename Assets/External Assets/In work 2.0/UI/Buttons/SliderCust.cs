using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderCust : MonoBehaviour {

    public Text sliderValue;

    public void Start() {
        valueUpdated();
    }

    public void valueUpdated() {

        sliderValue.text = this.GetComponent<Slider>().value.ToString() + " %";

    }

}
