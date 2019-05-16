using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject mainMenuGroup;
    public GameObject devOptionsGroup;
    public GameObject buildInfoText;
    public GameObject controlsText;
    public GameObject nextButton;
    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMainScene() {

        SceneManager.LoadScene("Main", LoadSceneMode.Single);

    }

    public void DevOptions(bool setVisible) {

        mainMenuGroup.SetActive(!setVisible);
        devOptionsGroup.SetActive(setVisible);

    }

    public void NextButtonPressed()
    {
        buildInfoText.SetActive(false);
        nextButton.SetActive(false);
        controlsText.SetActive(true);
        startButton.SetActive(true);
    }
}
