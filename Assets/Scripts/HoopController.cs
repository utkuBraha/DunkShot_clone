using Unity.VisualScripting;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    [SerializeField]private bool isRotating;
    private Transform centre;
    private Vector3 desiredPos;
    public float radius;
    public float rotationSpeed;
    private bool onMouseButtonDown;

    public Transform mousePosition;

    public bool IsRotating
    {
        get => isRotating;
        set
        {
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
        if (GameManager.Instance.IsPointerOverUIElement())
        {
            return;
        }

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
            mousePosition.position = Input.mousePosition;
            // float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            //transform.RotateAround(centre.position, Vector3.forward, rotationX);
               
            RotateHoop(Input.mousePosition);
        }
    }
    

    void RotateHoop(Vector3 mousePos)
    {
        // Get the world position of the mouse cursor
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));

        // Calculate the direction vector from the hoop's position to the mouse position
        Vector3 direction = worldMousePos - transform.position;

        // Calculate the angle between the direction vector and the hoop's local up vector (Z-axis)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the hoop around its local Z-axis
        transform.localRotation = Quaternion.Euler(0, 0, angle-270);
    }

}