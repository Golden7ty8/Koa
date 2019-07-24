using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    [Header("References:")]
    public Animator koaAnimator;
    public Rigidbody rb;
    public SensorGround groundSensorScript;
    public GameObject cameraPivot;
    public GameObject blackLight;

    [Header("Options:")]
    public bool horizontalMovement;
    public bool verticalMovement;
    public bool rotationalMovement;
    //public bool useMenuControls;
    public float walkSpeed;
    public float runSpeedMult;
    public float crouchSpeedMult;
    public float jumpHeight;
    public Vector3 spawnPoint;
    public float rotationaljumpAngle;
    public Vector3 startPoint;
    public bool useStartPoint;
    public bool useSpawnPoint;

    [Header("Debug Options:")]
    public float runAnimTimerLength;
    public float delayedJumpWindowBefore;
    public float delayedJumpWindowAfter;
    public float jumpVelocityCutMult;
    public float jumpReloadTime;

    float effectiveSpeedMult;
    bool dir;
    float runAnimTimer;
    float jumpInputDelayTimer;
    float groundeddelayTimer;
    float jumpReloadTimer;

    //bool inputjump;

    //bool j;

    // Start is called before the first frame update
    void Start()
    {
        dir = true;
        runAnimTimer = 0;
        jumpInputDelayTimer = 0;
        groundeddelayTimer = 0;
        jumpReloadTimer = 0;

        if(useStartPoint)
            transform.position = startPoint;
        if (!useSpawnPoint)
            spawnPoint = transform.position;

        //if (useMenuControls) {

            //Override untiy's inputs (Input.SetAxis("Vertical") = w, s)

        //}
    }

    void Update()
    {
        //if (Input.GetButtonDown("LightToggle"))
        //if (Input.GetKeyDown(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("black_light")).ToLower()))
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("black_light"))))
            blackLight.SetActive(!blackLight.activeSelf);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //KeyCode thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Whatever");

        //inputjump = Input.GetButtonDown("Jump");

        //Create input and other relavent variables.
        //float h = horizontalMovement ? Input.GetAxisRaw("Horizontal") : 0;
        //float h = horizontalMovement ? (Input.GetKey(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("walk_right")).ToLower()) ? 1 : 0) - (Input.GetKey(PlayerPrefs.GetString("walk_left").ToLower()) ? 1 : 0) : 0;
        float h = horizontalMovement ? (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walk_right"))) ? 1 : 0) - (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walk_left"))) ? 1 : 0) : 0;
        
        //float v = verticalMovement ? Input.GetAxisRaw("Vertical") : 0;
        //float v = verticalMovement ? (Input.GetKey(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("walk_forward")).ToLower()) ? 1 : 0) - (Input.GetKey(PlayerPrefs.GetString("walk_back").ToLower()) ? 1 : 0) : 0;
        float v = verticalMovement ? (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walk_forward"))) ? 1 : 0) - (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walk_back"))) ? 1 : 0) : 0;

        float ro = rotationalMovement ? Input.GetAxisRaw("Rotate") : 0;

        //Is Koa Running/Crouching/Jumping?
        //bool r = Input.GetButton("Run");
        //bool r = Input.GetKey(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("run")).ToLower());
        bool r = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("run")));
        
        //bool c = Input.GetButton("Crouch");
        //bool c = Input.GetKey(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("crouch")).ToLower());
        bool c = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouch")));

        //jumpInputDelayTimer = Input.GetButtonDown("Jump") ? delayedJumpWindow : (jumpInputDelayTimer > 0 && Input.GetButton("Jump") ? jumpInputDelayTimer - Time.deltaTime : 0);

        //if (Input.GetButton("Jump"))
        //if (Input.GetKey(ObjectNames.NicifyVariableName(PlayerPrefs.GetString("jump")).ToLower()))
        bool j = Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump")));
        if (j)
            jumpInputDelayTimer = delayedJumpWindowBefore;
        else {

            if (jumpInputDelayTimer > 0 && j)
                jumpInputDelayTimer = jumpInputDelayTimer - Time.deltaTime;
            else
                jumpInputDelayTimer = 0;

        }

        //jumpInputDelayTimer = Input.GetButton("Jump") ? delayedJumpWindow : (jumpInputDelayTimer > 0 ? jumpInputDelayTimer - Time.deltaTime : 0);
        bool jTimer = jumpInputDelayTimer > 0;

        groundeddelayTimer = groundSensorScript.isGrounded ? delayedJumpWindowAfter : (groundeddelayTimer > 0 ? groundeddelayTimer - Time.deltaTime : 0);
        bool isGrounded = groundeddelayTimer > 0;
        //bool isGrounded = groundSensorScript.isGrounded;

        //If moving right, Koa points right, if moving left, Koa points left, and
        //if not moving, Koa points the same direction as before.
        dir = h > 0 ? true : (h < 0 ? false : dir);

        //Multiply this with the walk speed to get the actual speed Koa is moving right now.
        effectiveSpeedMult = 1;

        //Tick run animation timer down (if applicable).
        if (runAnimTimer > 0)
            runAnimTimer -= Time.deltaTime;
        else
            runAnimTimer = 0;

        //Countdown the time until you can jump again after jumping.
        jumpReloadTimer = jumpReloadTimer > 0 ? jumpReloadTimer - Time.deltaTime : 0;

        //Rotate Koa
        if(Input.GetButtonDown("Rotate"))
            cameraPivot.transform.eulerAngles += new Vector3(0, Mathf.Round(ro) * rotationaljumpAngle, 0);

        //Set rotation
        transform.eulerAngles = new Vector3(0, cameraPivot.transform.eulerAngles.y + (dir ? 0 : 180), 0);

        //Move Koa
        if (h != 0 || v != 0)
        {

            koaAnimator.SetBool("isWalking", true);

            //Is Koa running?
            if (r)
            {
                koaAnimator.SetBool("isRunning", true);
                koaAnimator.SetBool("isCrouching", false);
                effectiveSpeedMult *= runSpeedMult;
                runAnimTimer = runAnimTimerLength;
            }
            else {
                koaAnimator.SetBool("isRunning", runAnimTimer > 0);

                //If Koa is not running, then check to see if crouching.
                if (c)
                {
                    koaAnimator.SetBool("isCrouching", true);
                    effectiveSpeedMult *= crouchSpeedMult;
                }
                else {
                    koaAnimator.SetBool("isCrouching", false);
                }
            }

            float dirVectorLength = Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2));

            rb.MovePosition(transform.position + transform.TransformDirection(new Vector3(h / dirVectorLength * walkSpeed * effectiveSpeedMult * Time.deltaTime * (dir ? 1 : -1), 0, v / dirVectorLength * walkSpeed * effectiveSpeedMult * Time.deltaTime * (dir ? 1 : -1))));

        }
        else {
            koaAnimator.SetBool("isWalking", false);
            koaAnimator.SetBool("isRunning", runAnimTimer > 0);

            if (c)
            {
                koaAnimator.SetBool("isCrouching", true);
                //effectiveSpeedMult *= crouchSpeedMult;
            }
            else
            {
                koaAnimator.SetBool("isCrouching", false);
            }
        }

        //Turn off Jumping animation upon landing.
        if (groundSensorScript.isGrounded) {
            koaAnimator.SetBool("isJumping", false);
        }

        //Should Koa Jump?
        /*if (j)
            //Debug.Log("jumpInputDelayTimer: " + jumpInputDelayTimer);*/
        if (jTimer && isGrounded && jumpReloadTimer <= 0)
        {

            //Proceed with jumping!
            //Debug.Log("Jumped!");
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            koaAnimator.SetBool("isJumping", true);

            //No extra window of time allowed where you can jump a second time!
            groundeddelayTimer = 0;

            jumpInputDelayTimer = 0;

            jumpReloadTimer = jumpReloadTime;

        }/* else if (Input.GetButton("Jump")) {

            print("DEBUG HERE!");

        }*/

        //If jump is not being held and you have upwards velocity, start cutting that upward velocity.
        //if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
        if (rb.velocity.y > 0 && !Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump")))) {

            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * (Time.deltaTime * jumpVelocityCutMult + (1 - Time.deltaTime) * 1), rb.velocity.z);
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * jumpVelocityCutMult, rb.velocity.z);

        }

    }

    public void KillKoa() {

        transform.position = spawnPoint;
        rb.velocity = Vector3.zero;

    }
}
