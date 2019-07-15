using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notification : MonoBehaviour {

    [Header("Achievement notification")]
    [SerializeField]
    private LocalizedText titleText;
    [SerializeField]
    private Image logoImage;
    private Animator animator;
    private static float pop_Length = 0.833f;
    [SerializeField]
    private float notificationShowTime = 10;
    private static float unPop_Length = 0.833f;

    public void SetNotification(string key, Sprite logo) {
        animator = GetComponent<Animator>();
        titleText.key = "achievement_" + key;
        logoImage.sprite = logo;
        StartCoroutine(NotificationPop());
    }

    private IEnumerator NotificationPop() {
        animator.Play("Notification_Pop");
        yield return new WaitForSecondsRealtime(pop_Length);
        yield return new WaitForSecondsRealtime(notificationShowTime);
        animator.Play("Notification_UnPop");
        yield return new WaitForSecondsRealtime(unPop_Length);
        Destroy(gameObject);
    }

}
