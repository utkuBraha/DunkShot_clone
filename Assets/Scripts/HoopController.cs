using UnityEngine;

public class HoopController : MonoBehaviour
{
    [SerializeField]private bool isRotating;
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;

    private Transform centre;
    private Vector3 desiredPos;
    private bool onMouseButtonDown;

    public bool IsRotating
    {
        get => isRotating;
        set
        {
            Debug.Log("IsRotating set to " + value);
            isRotating = value;
        }
    }

    void Start()
    {
        centre = transform;
        transform.position = (transform.position - centre.position).normalized * radius + centre.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onMouseButtonDown = false;
        }
        if (IsRotating && onMouseButtonDown)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(centre.position, Vector3.forward, rotationX);
        }
    }
}