using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BindingInfo : MonoBehaviour {

    public static BindingInfo bindingInfo_instance;

    [Header("Binding Infos")]
    public GameObject bindingPanel;
    public Text bindedToKey;
    public KeyCode escapeBinding;
    public GameObject BindingInfoList;
    public Text EscapeKey;

    public void Awake() {

        if(bindingInfo_instance == null) {
            bindingInfo_instance = this;
        }
        else if (bindingInfo_instance != null) {
            Destroy(gameObject);
        }

        if (escapeBinding != KeyCode.None) {
            EscapeKey.text = escapeBinding.ToString();
        }
    }

}
