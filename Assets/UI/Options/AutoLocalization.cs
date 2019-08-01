using UnityEngine;
using UnityEngine.UI;

public class AutoLocalization : MonoBehaviour {

    public void UpdateTextKey() {

        GetComponent<LocalizedText>().key = GetComponent<Text>().text;

    }

}
