using UnityEngine;

public class ColliderDeath : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            if (tag == "Water")
            {
                other.GetComponentInParent<ControllerPlayer>().KillKoa();
            } else
            {
                other.GetComponentInParent<ControllerPlayer>().KillKoa(1);
            }

        }
    }
}
