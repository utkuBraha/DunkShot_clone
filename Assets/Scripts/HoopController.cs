using System;
using System.Collections;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    [SerializeField]private bool isRotating;
    private Transform centre;
    private Vector3 desiredPos;
    public float radius;
    private bool onMouseButtonDown;
    public Transform mousePosition;
    public Ball ball;
    private bool isScoreIncreased;
    [SerializeField] private BoxCollider2D triggerExitCollider;
    public Transform frontHoop;
    public float extensionAmount = 0.02f;
    private bool hasExtended = false;
    private Vector3 originalScale;

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
        if (GameManager.Instance.IsPointerOverUIElement()&& !GameManager.Instance.isGameActive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            onMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse button up");
            onMouseButtonDown = false;
            if (ball != null)
            {
                Debug.Log("Ball is not null");
                ball.transform.SetParent(null);
                StartCoroutine(WaitAndSetTriggerValue());
            }
        }
        if (IsRotating && onMouseButtonDown)
        {
            mousePosition.position = Input.mousePosition;
            RotateHoop(Input.mousePosition);
        }
    }

    private IEnumerator WaitAndSetTriggerValue()
    {
        yield return new WaitForSeconds(.1f);
        triggerExitCollider.isTrigger = false;
    }
    void RotateHoop(Vector3 mousePos)
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
        Vector3 direction = worldMousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //
        if (!hasExtended && frontHoop != null)
        {
            Vector3 currentScale = frontHoop.localScale;
            currentScale.y += extensionAmount;
            frontHoop.localScale = currentScale;
            hasExtended = true; 
        }
        transform.localRotation = Quaternion.Euler(0, 0, angle-270);
    }
   
}