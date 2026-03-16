using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Makes the UI always face the main camera
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                         Camera.main.transform.rotation * Vector3.up);
    }
}