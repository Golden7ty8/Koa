using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CustomBinding : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [Header("Automatic")]
    public GameObject bindingPanel;
    public Text bindedToKey;
    public KeyCode escapeBinding;

    [Header("To set")]
    public Text nameBind;
    private string actor;

    private bool setBinding;
    public string inputToBindTo;
    public string currentBind;

    private bool waitClear = false;

    void Start() {
        gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
        if(PlayerPrefs.GetString(inputToBindTo) == "") {
            gameObject.GetComponentInChildren<Text>().text = "None";
        }
        currentBind = PlayerPrefs.GetString(inputToBindTo);

        bindingPanel = BindingInfo.bindingInfo_instance.bindingPanel;
        bindedToKey = BindingInfo.bindingInfo_instance.bindedToKey;
        escapeBinding = BindingInfo.bindingInfo_instance.escapeBinding;
    }

    public void newBinding() {
        bindingPanel.SetActive(true);
        if (Input.GetMouseButtonUp(0))
        {
            setBindingActor();

            //Debug.Log("Custom Binding start");
            setBinding = false;
            //gameObject.GetComponentInChildren<Text>().text = "";
            StartCoroutine(waitCustomBinding());
        }
    }

    IEnumerator waitCustomBinding() {
        yield return new WaitForFixedUpdate();
        //Debug.Log("Wait Binding !");
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            //Cancel
            if (Input.GetKeyDown(escapeBinding))
            {
                setBinding = true;
                bindingPanel.SetActive(false);
                Debug.Log("Cancel Binding");
                yield break;
            }
            //Set Binding
            if (Input.GetKeyDown(kcode) && setBinding == false)
            {
                Debug.Log("Custom binding for " + inputToBindTo + " : " + kcode);

                currentBind = kcode.ToString();

                PlayerPrefs.SetString(inputToBindTo, currentBind);
                gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
                setBinding = true;
                bindingPanel.SetActive(false);
                ReloadTutorialElements();
            }
        }
        if (setBinding == false)
        {
            StartCoroutine(waitCustomBinding());
            //Debug.Log("Restart Binding");
        }
    }

    public void setBindingActor() {
        actor = nameBind.text;
        bindedToKey.text = " \" " + actor + " \""; 
    }


    //Clear binding
    public void OnPointerEnter(PointerEventData pointerEventData) {
        Debug.Log("Wait for clear binding");
        waitClear = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        Debug.Log("Exit clear binding");
        waitClear = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && waitClear == true) {
            currentBind = "None";
            PlayerPrefs.SetString(inputToBindTo, currentBind);
            gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
            Debug.Log("Binding cleared");
            ReloadTutorialElements();
            
            waitClear = false;
        }

        if(PlayerPrefs.GetString(inputToBindTo) != gameObject.GetComponentInChildren<Text>().text) {
            gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
        }
    }

    public static void ReloadTutorialElements ()
    {
        //Only do this if we are in the "Level 1" scene
        if(SceneManager.GetActiveScene().name == "Level 1") {
            Tutorial_UpdateKeys.instance.ReloadTutorialText();
        }
    }

}
