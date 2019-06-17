using UnityEngine;
using System.Collections;

public class GameCore_Controls : MonoBehaviour {

    public static GameCore_Controls gameCore_Controls_instance;

    [Header("Controls")]
    //Movements
    private string walkForward;
    public KeyCode WalkForward;
    private string walkBack;
    public KeyCode WalkBack;
    private string walkLeft;
    public KeyCode WalkLeft;
    private string walkRight;
    public KeyCode WalkRight;
    private string crouch;
    public KeyCode Crouch;
    private string jump;
    public KeyCode Jump;
    private string run;
    public KeyCode Run;
    //Capacities
    private string attack;
    public KeyCode Attack;
    private string black_light;
    public KeyCode Black_light;
    //Menus
    private string pauseMenu;
    public KeyCode PauseMenu;

    public void Awake() {
        if(gameCore_Controls_instance == null) {
            gameCore_Controls_instance = this;
        }
        else if (gameCore_Controls_instance != null) {
            Destroy(gameObject);
        }

        Debug.Log("<color=green>Load of controls</color>");
        //Check if the controls have already been set a first time and aren't empty by default
        if(PlayerPrefs.GetString("controlsFirstSet") != "Done") {
            firstSetControls();
        }
        else { //The controls have already been set a first time and players can choose to keep certain keys empty
            reloadControls();
        }
        //Future controls will have to been set by a specific update variables script at the launch
    }

    public void firstSetControls() {
        //Movements
        if (PlayerPrefs.GetString("walk_forward") == "" || PlayerPrefs.GetString("walk_back") == "None") {
            Debug.Log("Reset walk_forward");
            PlayerPrefs.SetString("walk_forward", "W");
        }
        if (PlayerPrefs.GetString("walk_back") == "" || PlayerPrefs.GetString("walk_back") == "None") {
            PlayerPrefs.SetString("walk_back", "S");
        }
        if (PlayerPrefs.GetString("walk_left") == "" || PlayerPrefs.GetString("walk_left") == "None") {
            PlayerPrefs.SetString("walk_left", "A");
        }
        if (PlayerPrefs.GetString("walk_right") == "" || PlayerPrefs.GetString("walk_right") == "None") {
            PlayerPrefs.SetString("walk_right", "D");
        }
        if (PlayerPrefs.GetString("crouch") == "" || PlayerPrefs.GetString("crouch") == "None") {
            PlayerPrefs.SetString("crouch", "LeftShift");
        }
        if (PlayerPrefs.GetString("jump") == "" || PlayerPrefs.GetString("jump") == "None") {
            PlayerPrefs.SetString("jump", "Space");
        }
        if (PlayerPrefs.GetString("run") == "" || PlayerPrefs.GetString("run") == "None") {
            PlayerPrefs.SetString("run", "R");
        }
        //Capacities
        if (PlayerPrefs.GetString("attack") == "" || PlayerPrefs.GetString("attack") == "None") {
            PlayerPrefs.SetString("attack", "E");
        }
        if (PlayerPrefs.GetString("black_light") == "" || PlayerPrefs.GetString("black_light") == "None") {
            PlayerPrefs.SetString("black_light", "A");
        }
        //Menus
        if (PlayerPrefs.GetString("pauseMenu") == "" || PlayerPrefs.GetString("pauseMenu") == "None") {
            PlayerPrefs.SetString("pauseMenu", "Escape");
        }

        PlayerPrefs.SetString("controlsFirstSet", "Done");

        reloadControls();
    }

    public void reloadControls() {
        //Movements
        walkForward = PlayerPrefs.GetString("walk_forward");
        WalkForward = (KeyCode)System.Enum.Parse(typeof(KeyCode), walkForward);
        walkBack = PlayerPrefs.GetString("walk_back");
        WalkBack = (KeyCode)System.Enum.Parse(typeof(KeyCode), walkBack);
        walkLeft = PlayerPrefs.GetString("walk_left");
        WalkLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), walkLeft);
        walkRight = PlayerPrefs.GetString("walk_right");
        WalkRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), walkRight);
        crouch = PlayerPrefs.GetString("crouch");
        Crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), crouch);
        jump = PlayerPrefs.GetString("jump");
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), jump);
        run = PlayerPrefs.GetString("run");
        Run = (KeyCode)System.Enum.Parse(typeof(KeyCode), run);
        //Capacities
        attack = PlayerPrefs.GetString("attack");
        Attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), attack);
        black_light = PlayerPrefs.GetString("black_light");
        Black_light = (KeyCode)System.Enum.Parse(typeof(KeyCode), black_light);
        //Menus
        pauseMenu = PlayerPrefs.GetString("pauseMenu");
        PauseMenu = (KeyCode)System.Enum.Parse(typeof(KeyCode), pauseMenu);
        Debug.Log("<color=green>Keys reloaded</color>");
    }

    public void resetControls() {
        //Movements
        PlayerPrefs.DeleteKey("walk_forward");
        PlayerPrefs.DeleteKey("walk_back");
        PlayerPrefs.DeleteKey("walk_left");
        PlayerPrefs.DeleteKey("walk_right");
        PlayerPrefs.DeleteKey("crouch");
        PlayerPrefs.DeleteKey("jump");
        PlayerPrefs.DeleteKey("run");
        //Capacities
        PlayerPrefs.DeleteKey("attack");
        PlayerPrefs.DeleteKey("black_light");
        //Menus
        PlayerPrefs.DeleteKey("pauseMenu");

        Debug.Log("<color=green>Controls have been reset</color>");
        firstSetControls();
    }

}
