using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomBinding : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject BindingInfo;
    public Text bindedForInfo;
    public Text nameBind;
    private string actor;

    private bool setBinding;
    public string inputToBindTo;
    public string currentBind;

    private bool waitClear = false;

    void Start()
    {
        gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
        currentBind = PlayerPrefs.GetString(inputToBindTo);
    }

    public void newBinding()
    {
        BindingInfo.SetActive(true);
        if (Input.GetMouseButtonUp(0))
        {
            setBindingActor();

            //Debug.Log("Custom Binding start");
            setBinding = false;
            //gameObject.GetComponentInChildren<Text>().text = "";
            StartCoroutine(waitCustomBinding());
        }
    }

    IEnumerator waitCustomBinding()
    {
        yield return new WaitForFixedUpdate();
        //Debug.Log("Wait Binding !");
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            //Set Binding
            if (Input.GetKeyDown(kcode) && setBinding == false)
            {
                Debug.Log("Custom binding for " + inputToBindTo + " : " + kcode);

                currentBind = kcode.ToString();

                PlayerPrefs.SetString(inputToBindTo, currentBind);
                gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
                setBinding = true;
                BindingInfo.SetActive(false);
            }
            //Cancel
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                setBinding = true;
                BindingInfo.SetActive(false);
                Debug.Log("Cancel Binding");
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
        bindedForInfo.text = " \" " + actor + " \""; 
    }


    //Clear binding
    public void OnPointerEnter(PointerEventData pointerEventData) {
        Debug.Log("Wait for clear binding");
        waitClear = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Exit clear binding");
        waitClear = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && waitClear == true) {
            currentBind = "None";
            PlayerPrefs.SetString(inputToBindTo, currentBind);
            gameObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(inputToBindTo);
            Debug.Log("Binding cleared");
            
            waitClear = false;
        }
    }

}
