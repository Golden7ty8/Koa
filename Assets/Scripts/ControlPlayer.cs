using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [Header("References")]
    public Animator koaAnimator;
    public Rigidbody rb;

    [Header("Options")]
    public float walkSpeed;
    public float runSpeedMult;
    public float crouchSpeedMult;

    float effectiveSpeedMult;
    bool dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Create input and other relavent variables.
        float h = Input.GetAxisRaw("Horizontal");

        //Is Koa Running/Crouching?
        bool r = Input.GetButton("Run");
        bool c = Input.GetButton("Crouch");

        //If moving right, Koa points right, if moving left, Koa points left, and
        //if not moving, Koa points the same direction as before.
        dir = h > 0 ? true : (h < 0 ? false : dir);

        //Multiply this with the walk speed to get the actual speed Koa is moving right now.
        effectiveSpeedMult = 1;

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
            }
            else {
                koaAnimator.SetBool("isRunning", false);

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
            koaAnimator.SetBool("isRunning", false);

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

    }
}
