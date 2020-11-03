using UnityEngine;

public class RotateTowardCamera : MonoBehaviour
{
    private void Update()
    {
        Quaternion camrot = Camera.main.transform.rotation;
        transform.rotation = camrot;
    }
}
