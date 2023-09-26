using UnityEngine;

public class HoopController : MonoBehaviour
{
    [SerializeField]private bool isRotating;
    private Transform centre;
    private Vector3 desiredPos;
    public float radius;
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

        if (IsRotating && onMouseButtonDown )
        {
            mousePosition.position = Input.mousePosition;
            RotateHoop(Input.mousePosition);
            
        }
    }
    void RotateHoop(Vector3 mousePos)
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
        Vector3 direction = worldMousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle-270);
    }
    // private bool canRotate = true;
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("triggerexit"))
    //     {
    //         canRotate = false;
    //     }
    // }
}