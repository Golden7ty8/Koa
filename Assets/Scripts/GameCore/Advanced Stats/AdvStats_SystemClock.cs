using UnityEngine;
using UnityEngine.UI;
using System;

public class AdvStats_SystemClock : MonoBehaviour {

    [SerializeField]
    private Text systemClockDisplay;

    private void LateUpdate() {
        systemClockDisplay.text = DateTime.Now.ToLocalTime().ToShortTimeString();
    }

}
