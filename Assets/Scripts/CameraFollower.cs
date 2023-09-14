using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.8f;
    private Vector3 _offset;
    void Start()
    {
        if (target != null)
        {
            _offset = transform.position - target.position;
        }
    }
    private void LateUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition = target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}