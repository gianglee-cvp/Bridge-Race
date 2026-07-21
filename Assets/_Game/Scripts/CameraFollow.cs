using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 move = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            move,
            smoothSpeed * Time.deltaTime
        );
    }
}