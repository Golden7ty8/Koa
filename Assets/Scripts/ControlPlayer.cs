using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [Header("References:")]
    public Animator koaAnimator;
    public Rigidbody rb;
    public SensorGround groundSensorScript;

    [Header("Options:")]
    public float walkSpeed;
    public float runSpeedMult;
    public float crouchSpeedMult;
    public float jumpHeight;

    [Header("Debug Options:")]
    public float runAnimTimerLength;
    public float delayedJumpWindow;
    public float jumpVelocityCutMult;
    public float jumpReloadTime;

    float effectiveSpeedMult;
    bool dir;
    float runAnimTimer;
    float jumpInputDelayTimer;
    float groundeddelayTimer;
    float jumpReloadTimer;

    // Start is called before the first frame update
    void Start()
    {
        dir = true;
        runAnimTimer = 0;
        jumpInputDelayTimer = 0;
        groundeddelayTimer = 0;
        jumpReloadTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Create input and other relavent variables.
        float h = Input.GetAxisRaw("Horizontal");

        //Is Koa Running/Crouching/Jumping?
        bool r = Input.GetButton("Run");
        bool c = Input.GetButton("Crouch");

        //jumpInputDelayTimer = Input.GetButtonDown("Jump") ? delayedJumpWindow : (jumpInputDelayTimer > 0 ? jumpInputDelayTimer - Time.deltaTime : 0);
        jumpInputDelayTimer = Input.GetButton("Jump") ? delayedJumpWindow : (jumpInputDelayTimer > 0 ? jumpInputDelayTimer - Time.deltaTime : 0);
        bool j = jumpInputDelayTimer > 0;

        groundeddelayTimer = groundSensorScript.isGrounded ? delayedJumpWindow : (groundeddelayTimer > 0 ? groundeddelayTimer - Time.deltaTime : 0);
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

        //Move Koa
        if (h != 0)
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
            rb.MovePosition(transform.position + new Vector3(h * walkSpeed * effectiveSpeedMult * Time.deltaTime, 0, 0));

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

        //Set rotation
        transform.eulerAngles = new Vector3(0, dir ? 0 : 180, 0);

        //Turn off Jumping animation upon landing.
        if (groundSensorScript.isGrounded) {
            koaAnimator.SetBool("isJumping", false);
        }

        //Should Koa Jump?
        if (j && isGrounded && jumpReloadTimer <= 0) {

            //Proceed with jumping!
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            koaAnimator.SetBool("isJumping", true);

            //No extra window of time allowed where you can jump a second time!
            groundeddelayTimer = 0;

            jumpInputDelayTimer = 0;

            jumpReloadTimer = jumpReloadTime;

        }

        //If jump is not being held and you have upwards velocity, start cutting that upward velocity.
        /*if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {

            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * (Time.deltaTime * jumpVelocityCutMult + (1 - Time.deltaTime) * 1), rb.velocity.z);

        }*/

    }
}
