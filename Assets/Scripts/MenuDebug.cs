using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct DebugStats<T> {

    public T value;
    public string name;
    public T defaultValue;

};

public class MenuDebug : MonoBehaviour
{

    [Header("References:")]
    public Text displayText;
    public GameObject inputPlaceholder;
    public GameObject debugGroup;
    public ControllerPlayer controllerPlayer;
    public BatteryLifeManager batteryLifeManager;

    [Header("Options:")]
    //public bool inputStyle;
    public bool visible;

    int optionCount;
    int index;
    float jumpHeightDefault, walkSpeedDefault, runSpeedMultDefault, crouchSpeedMultDefault, batteryDrainRateDefault, batteryRechargeRateDefault;

    // Start is called before the first frame update
    void Start()
    {
        optionCount = 6;
        index = 0;

        jumpHeightDefault = controllerPlayer.jumpHeight;
        walkSpeedDefault = controllerPlayer.walkSpeed;
        runSpeedMultDefault = controllerPlayer.runSpeedMult;
        crouchSpeedMultDefault = controllerPlayer.crouchSpeedMult;
        batteryDrainRateDefault = batteryLifeManager.drainRate;
        batteryRechargeRateDefault = batteryLifeManager.rechargeRate;

        //inputDisplay.SetActive(inputStyle);
    }

    // Update is called once per frame
    void Update()
    {

        //if (inputStyle)
            InputStyle();
        //else
            //IncrementStyle ();
        
    }

    void InputStyle()
    {

        if (Input.GetButtonDown("DebugToggle"))
            visible = !visible;

        if (visible)
        {

            if (Input.GetButtonDown("DebugHorizontal"))
            {

                if (Input.GetAxisRaw("DebugHorizontal") > 0)
                    index++;
                else if (Input.GetAxisRaw("DebugHorizontal") < 0)
                    index--;

                index = ((index + 1) % optionCount) - 1;

                while (index < 0)
                    index += optionCount;

            }

            DebugStats<float> tmp = GetValueWithIndex(index);

            if (Input.GetButtonDown("DebugVertical"))
            {

                if (Input.GetAxisRaw("DebugVertical") > 0)
                    SetValueWithIndex(index, 0.1f, true);
                else if (Input.GetAxisRaw("DebugVertical") < 0)
                    SetValueWithIndex(index, -0.1f, true);

            }

            if (Input.GetButtonDown("DebugDefault"))
                SetValueWithIndex(index, 0, false, true);

            displayText.text = "◀  " + (index + 1) + ": " + tmp.name + "  ▶\n\n" + tmp.value;
            inputPlaceholder.GetComponent<Text>().text = "Default Value: " + tmp.defaultValue;

            debugGroup.gameObject.SetActive(true);

        }
        else
        {

            debugGroup.gameObject.SetActive(false);

        }

    }

    /*void IncrementStyle()
    {

        if (Input.GetButtonDown("DebugToggle"))
            visible = !visible;

        if (visible)
        {

            if (Input.GetButtonDown("DebugHorizontal"))
            {

                if (Input.GetAxisRaw("DebugHorizontal") > 0)
                    index++;
                else if (Input.GetAxisRaw("DebugHorizontal") < 0)
                    index--;

                index = ((index + 1) % optionCount) - 1;

                while (index < 0)
                    index += optionCount;

            }

            DebugStats<float> tmp = GetValueWithIndex(index);

            if (Input.GetButtonDown("DebugVertical"))
            {

                if (Input.GetAxisRaw("DebugVertical") > 0)
                    SetValueWithIndex(index, 0.1f, true);
                else if (Input.GetAxisRaw("DebugVertical") < 0)
                    SetValueWithIndex(index, -0.1f, true);

            }

            if (Input.GetButtonDown("DebugDefault"))
                SetValueWithIndex(index, 0, false, true);

            displayText.text = index + 1 + ": " + tmp.name + "\n\n" + tmp.value;

            debugGroup.gameObject.SetActive(true);

        }
        else
        {

            debugGroup.gameObject.SetActive(false);

        }

    }*/

    public void ReceiveInput(string input) {

        //float inputFloat;

        if (input == "")
            SetValueWithIndex(index, 0, false, true);
        else
            SetValueWithIndex(index, float.Parse(input), false);

    }

    DebugStats<float> GetValueWithIndex(int i) {

        //i = i % 0;
        DebugStats<float> tmp = new DebugStats<float>();

        switch (i) {

            case 0:
                tmp.value = controllerPlayer.jumpHeight;
                tmp.name = "Jump Height";
                tmp.defaultValue = jumpHeightDefault;
                return tmp;
            case 1:
                tmp.value = controllerPlayer.walkSpeed;
                tmp.name = "Walk Speed";
                tmp.defaultValue = walkSpeedDefault;
                return tmp;
            case 2:
                tmp.value = controllerPlayer.runSpeedMult;
                tmp.name = "Run Speed Multiplier";
                tmp.defaultValue = runSpeedMultDefault;
                return tmp;
            case 3:
                tmp.value = controllerPlayer.crouchSpeedMult;
                tmp.name = "Crouch Speed Multiplier";
                tmp.defaultValue = crouchSpeedMultDefault;
                return tmp;
            case 4:
                tmp.value = batteryLifeManager.drainRate;
                tmp.name = "Blacklight Drain Rate";
                tmp.defaultValue = batteryDrainRateDefault;
                return tmp;
            case 5:
                tmp.value = batteryLifeManager.rechargeRate;
                tmp.name = "Blacklight Recharge Rate";
                tmp.defaultValue = batteryRechargeRateDefault;
                return tmp;

            default:
                tmp.name = "ERROR";
                return tmp;

        }

    }

    /*void IncreaseValueWithIndex(int i, float amount) {

        switch (i) {

            case 0:
                controllerPlayer.jumpHeight += amount;
                break;
            case 1:
                controllerPlayer.walkSpeed += amount;
                break;
            case 2:
                controllerPlayer.runSpeedMult += amount;
                break;
            case 3:
                controllerPlayer.crouchSpeedMult += amount;
                break;

        }

    }*/

    void SetValueWithIndex(int i, float amount, bool additive, bool setDefault = false)
    {

        switch (i)
        {

            case 0:
                if (setDefault)
                    amount = jumpHeightDefault;
                if (!additive)
                    amount -= controllerPlayer.jumpHeight;
                controllerPlayer.jumpHeight += amount;
                /*if (controllerPlayer.jumpHeight % 0.1 >= 0.05)
                    controllerPlayer.jumpHeight += 1 - controllerPlayer.jumpHeight % 0.1f;
                else*/
                    controllerPlayer.jumpHeight = Mathf.Round(controllerPlayer.jumpHeight * 10)/10;
                break;
            case 1:
                if (setDefault)
                    amount = walkSpeedDefault;
                if (!additive)
                    amount -= controllerPlayer.walkSpeed;
                controllerPlayer.walkSpeed += amount;
                //controllerPlayer.walkSpeed -= controllerPlayer.walkSpeed % 0.1f;
                controllerPlayer.walkSpeed = Mathf.Round(controllerPlayer.walkSpeed * 10) / 10;
                break;
            case 2:
                if (setDefault)
                    amount = runSpeedMultDefault;
                if (!additive)
                    amount -= controllerPlayer.runSpeedMult;
                controllerPlayer.runSpeedMult += amount;
                //controllerPlayer.runSpeedMult -= controllerPlayer.runSpeedMult % 0.1f;
                controllerPlayer.runSpeedMult = Mathf.Round(controllerPlayer.runSpeedMult * 10) / 10;
                break;
            case 3:
                if (setDefault)
                    amount = crouchSpeedMultDefault;
                if (!additive)
                    amount -= controllerPlayer.crouchSpeedMult;
                controllerPlayer.crouchSpeedMult += amount;
                //controllerPlayer.crouchSpeedMult -= controllerPlayer.cr % 0.1f;
                controllerPlayer.crouchSpeedMult = Mathf.Round(controllerPlayer.crouchSpeedMult * 10) / 10;
                break;
            case 4:
                if (setDefault)
                    amount = batteryDrainRateDefault;
                if (!additive)
                    amount -= batteryLifeManager.drainRate;
                batteryLifeManager.drainRate += amount;
                //controllerPlayer.crouchSpeedMult -= controllerPlayer.cr % 0.1f;
                if(additive)
                    batteryLifeManager.drainRate = Mathf.Round(controllerPlayer.crouchSpeedMult * 10) / 10;
                break;
            case 5:
                if (setDefault)
                    amount = batteryRechargeRateDefault;
                if (!additive)
                    amount -= batteryLifeManager.rechargeRate;
                batteryLifeManager.rechargeRate += amount;
                //controllerPlayer.crouchSpeedMult -= controllerPlayer.cr % 0.1f;
                if(additive)
                    batteryLifeManager.rechargeRate = Mathf.Round(controllerPlayer.crouchSpeedMult * 10) / 10;
                break;

        }

    }
}
