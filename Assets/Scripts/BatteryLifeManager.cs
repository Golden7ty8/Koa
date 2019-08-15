using UnityEngine;
using UnityEngine.UI;

public class BatteryLifeManager : MonoBehaviour
{
    [Header("References")]
    public GameObject blacklight;
    public Image meterUI;

    [Header("Options")]
    public float drainRate;
    public float rechargeRate;

    float meterRatio = 1;

    void Update() {
        if(blacklight.activeSelf) {
            meterRatio -= Time.deltaTime * drainRate;

            if (meterRatio < 0)
                meterRatio = 0;
        }
        else {
            meterRatio += Time.deltaTime * rechargeRate;

            if (meterRatio > 1)
                meterRatio = 1;
        }

        //Vector3 tmp = meterUI.GetComponent<RectTransform>().localScale;
        meterUI.fillAmount = meterRatio;
        //meterUI.GetComponent<RectTransform>().localScale = new Vector3(meterRatio, 1, 1);

        if (meterRatio <= 0) {
            blacklight.SetActive(false);
        }
    }
}
