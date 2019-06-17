using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{

    public string key;
    private GameObject instance;

    // Use this for initialization
    void Update()
    {
        instance = GameObject.Find("Language");
        Text text = GetComponent<Text>();
        text.text = instance.GetComponent<LocalizationManager>().GetLocalizedValue(key);
    }

}