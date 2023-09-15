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
            var transformPosition = transform.position;
            transformPosition.y = Mathf.Lerp(transformPosition.y, desiredPosition.y, smoothSpeed);
            transform.position = transformPosition;
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}


