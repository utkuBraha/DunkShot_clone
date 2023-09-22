using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;

    private Transform centre;
    private Vector3 desiredPos;
    private bool isRotating = false;
    private bool onMouseButtonDown;

    public bool IsRotating
    {
        get => isRotating;
        set => isRotating = value;
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

            // desiredPos = (transform.position - centre.position).normalized * radius + centre.position;
            // transform.position = Vector3.MoveTowards(transform.position, desiredPos, radiusSpeed * Time.deltaTime);
        }
    }
}
