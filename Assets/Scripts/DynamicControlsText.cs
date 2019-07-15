using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicControlsText : MonoBehaviour
{

    public bool twoControls;
    public string beforeText;
    public string controlName1;
    public string betweenText;
    public string controlName2;
    public string afterText;
    
    // Start is called before the first frame update
    void Start()
    {
        Text tmp = GetComponent<Text>();

        tmp.text = beforeText;
        tmp.text += PlayerPrefs.GetString(controlName1);
        if (twoControls)
        {
            tmp.text += betweenText;
            tmp.text += PlayerPrefs.GetString(controlName2);
        }
        tmp.text += afterText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
