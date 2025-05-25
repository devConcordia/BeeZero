using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] public Transform target;  // o player
    [SerializeField] public Vector3 offset;    // distância entre câmera e player
	[SerializeField] public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
