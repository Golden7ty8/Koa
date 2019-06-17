using UnityEngine;
using UnityEngine.UI;
using System;

public class Stat_SystemClock : MonoBehaviour {

    public Text systemClockStat;

    private void LateUpdate() {
        string clockHour = DateTime.Now.ToLocalTime().ToShortTimeString();
        systemClockStat.text = clockHour;
    }

}
