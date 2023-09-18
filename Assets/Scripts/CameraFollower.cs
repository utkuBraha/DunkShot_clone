using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.8f;
    [SerializeField] private Vector3 _offset;
    void Start()
    {
        if (target != null)
        {
            _offset = transform.position - target.position;
        }
    } private void LateUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition = target.position + _offset;
            if (desiredPosition.y > transform.position.y)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, desiredPosition.y, transform.position.z), smoothSpeed);
            }
        }
    }
}