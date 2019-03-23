using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

    public Camera playerCamera;
    public Transform Player;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    private void Update() {

        if(playerCamera && Player) {

            Vector3 point = playerCamera.WorldToViewportPoint(Player.position);
            Vector3 delta = Player.position - playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

        }
        
    }

}
