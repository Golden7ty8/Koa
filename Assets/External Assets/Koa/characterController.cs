using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

    [Header("Character sprites")]
    public GameObject origin;

    [Header("Character effects")]

    [Header("Character controls")]
    public string keyRight;
    private KeyCode KeyRight;
    public string keyLeft;
    private KeyCode KeyLeft;
    public string keyRun;
    private KeyCode KeyRun;
    public string keyJump;
    private KeyCode KeyJump;
    public string keyCrouch;
    private KeyCode KeyCrouch;

    public bool toRight = true;

    public bool isLanded; //Not used

    [Header("Character stats")]
    public float originWalkSpeed = 5f;
    private float walkSpeed;
    public float originRunSpeed = 8f;
    private float runSpeed;

    public float rotationSpeed = 0f;

    public float originJumpForce = 100f;
    private float jumpForce;

    public float crouchSlowdown = 0.5f;
    public float actualCrouchSlowdown = 1f;

    [Header("Character animations")]
    public Animator KoaAnimator;

    private void Start() {
        KoaAnimator = origin.GetComponent<Animator>();
        walkSpeed = originWalkSpeed;
        runSpeed = originRunSpeed;
        jumpForce = originJumpForce;
        actualCrouchSlowdown = 1f;
    }

    private void Update() {

        ReloadControls(); //Unoptimized but to make the script for the test more simple. I think there better be a script for the options menu and when you save or exit it it calls that reload controls for the player

        //Origin GameObject rotation
        if(toRight) {
            origin.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else
        {
            origin.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        //Crouch
        if (Input.GetKeyDown(KeyCrouch))
        {
            KoaAnimator.SetBool("isCrouching", true);
            actualCrouchSlowdown = crouchSlowdown;
        }
        if (Input.GetKeyUp(KeyCrouch) && actualCrouchSlowdown == crouchSlowdown)
        {
            KoaAnimator.SetBool("isCrouching", false);
            actualCrouchSlowdown = 1f;
        }

        //Walk right
        if (Input.GetKey(KeyRight)) {
            toRight = true;
            KoaAnimator.SetBool("isWalking", true);
            transform.Translate(Vector3.right * walkSpeed * actualCrouchSlowdown * Time.deltaTime, Space.World);
        }
        if(Input.GetKeyUp(KeyRight)) {
            KoaAnimator.SetBool("isWalking", false);
        }

        //Walk left
        if (Input.GetKey(KeyLeft)) {
            toRight = false;
            KoaAnimator.SetBool("isWalking", true);
            transform.Translate(Vector3.left * walkSpeed *actualCrouchSlowdown * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyUp(KeyLeft)) {
            KoaAnimator.SetBool("isWalking", false);
        }

        //Not running anymore
        if (KoaAnimator.GetBool("isWalking") == false || Input.GetKeyUp(KeyRun) || actualCrouchSlowdown == crouchSlowdown)
        {
            KoaAnimator.SetBool("isRunning", false);
        }

        //Run right
        if (Input.GetKey(KeyRun) && Input.GetKey(KeyRight) && actualCrouchSlowdown != crouchSlowdown) {
            toRight = true;
            KoaAnimator.SetBool("isRunning", true);
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime, Space.World);
        }

        //Run left
        if (Input.GetKey(KeyRun) && Input.GetKey(KeyLeft) && actualCrouchSlowdown != crouchSlowdown) {
            toRight = false;
            KoaAnimator.SetBool("isRunning", true);
            transform.Translate(Vector3.left * runSpeed * Time.deltaTime, Space.World);
        }

        //Jump
        if (Input.GetKeyDown(KeyJump)) {
            KoaAnimator.SetBool("isJumping", true);
            transform.Translate(Vector3.up * jumpForce * actualCrouchSlowdown * Time.deltaTime, Space.World);
        }
        if(Input.GetKeyUp(KeyJump)) {
            KoaAnimator.SetBool("isJumping", false);
        }

    }

    //Reload controls
    public void ReloadControls()
    {

        keyRight = PlayerPrefs.GetString("keyRight");
        KeyRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyRight);

        keyLeft = PlayerPrefs.GetString("keyLeft");
        KeyLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyLeft);

        keyRun = PlayerPrefs.GetString("keyRun");
        KeyRun = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyRun);

        keyJump = PlayerPrefs.GetString("keyJump");
        KeyJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyJump);

        keyCrouch = PlayerPrefs.GetString("keyCrouch");
        KeyCrouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCrouch);

    }

}
