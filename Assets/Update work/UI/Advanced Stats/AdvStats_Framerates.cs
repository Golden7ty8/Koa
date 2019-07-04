using UnityEngine;
using UnityEngine.UI;

public class AdvStats_Framerates : MonoBehaviour {

    private float deltaTime = 0.0f;
    [SerializeField]
    private Text frameratesDisplay;

    private void Update() {

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        frameratesDisplay.text = string.Format("{0:0.}", fps);
    }

}
