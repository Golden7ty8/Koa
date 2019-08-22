using UnityEngine;

public class HideGUI : MonoBehaviour {

    [SerializeField]
    private KeyCode hideGUIkey;
    private bool showGUI = true;
    [SerializeField]
    private GameObject[] gui;

    private void LateUpdate() {

        if(Input.GetKeyDown(hideGUIkey)) {
            if(showGUI) {
                Hide_GUI();
            }
            else {
                Show_GUI();
            }
        }

    }

    private void Hide_GUI() {
        showGUI = false;
        Cursor.visible = false;
        for (int i = 0; i < gui.Length; i++) {
            gui[i].SetActive(false);
        }
    }

    private void Show_GUI() {
        showGUI = true;
        Cursor.visible = true;
        for (int i = 0; i < gui.Length; i++) {
            gui[i].SetActive(true);
        }
    }

}
