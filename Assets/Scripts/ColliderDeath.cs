using UnityEngine;

public class ColliderDeath : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            other.GetComponentInParent<ControllerPlayer>().KillKoa();

        }
    }
}
