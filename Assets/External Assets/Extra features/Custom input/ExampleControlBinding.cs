using UnityEngine;

public class ExampleBinding : MonoBehaviour {

    public string keyTest;
    private KeyCode KeyTest;

    private void Start() {

        ReloadControls();
        
    }

    private void Update() {

        if(Input.GetKeyDown(KeyTest)) {

            Debug.Log("<b>Text binding !</b>");

        }
        
    }

    public void ReloadControls() {

        keyTest = PlayerPrefs.GetString("keyTest");
        KeyTest = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyTest);

    }

}
