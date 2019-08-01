using UnityEngine;

public class Credits : MonoBehaviour {

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("CanSlide", true);
    }

    public void SetThankYou() {
        AdvancedGameUI.instance.GetAchievementManager().SetAchievementCompleted("Credits_ThankYou");
    }

}
