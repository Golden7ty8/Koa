using UnityEngine;

public class Koa_SetupExample : MonoBehaviour {

    [Header("Koa Setup")]
    [SerializeField]
    private bool isFacingRight = true;
    [Header("Press in runtime to update the example")]
    [SerializeField]
    private bool update;
    //Note : this update system is only good for a preview but NOT optimized at all for an other usage

    private void Start() {
        SetFacing(isFacingRight);
    }

    private void LateUpdate() {
        if(update) {
            SetFacing(isFacingRight);
        }
    }

    private void SetFacing(bool IsFacingRight) {
        if(isFacingRight) {
            transform.localScale = Vector3.one;
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
