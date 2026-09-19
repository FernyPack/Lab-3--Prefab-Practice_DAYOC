using UnityEngine;

public class HealthBarFaceCamera : MonoBehaviour
{
    public Transform playerCamera;

    void Update()
    {
        if(playerCamera != null)
            transform.LookAt(transform.position + playerCamera.forward);
    }
}
