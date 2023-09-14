using System;
using UnityEngine;


public class DragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rigidbody2D;
    public float draglimit = 5f;
    public float forceAdd = 10f;
    private Camera mainCamera;
    private bool isDragging;

    private void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.enabled = false;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            DragStart();
        }

        if (isDragging)
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragEnd();
        }
    }

    Vector3 GetMousePosition 
    {
    get 
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}

    void DragStart()
    {
            lineRenderer.enabled = true;
            isDragging = true;
            lineRenderer.SetPosition(0,GetMousePosition);
    }

    void Drag()
    {
            Vector3 startPosition = lineRenderer.GetPosition(0);
            Vector3 currentPosition = GetMousePosition;
            Vector3 direction = currentPosition - startPosition;
            if (direction.magnitude <= draglimit)
            {
                lineRenderer.SetPosition(1, currentPosition);
            }
            else
            {
                Vector3 dragPosition = startPosition + direction.normalized * draglimit;
                lineRenderer.SetPosition(1,dragPosition);

            }
            
    }
    private void DragEnd()
    {
            isDragging = false;
            lineRenderer.enabled = false;
            Vector3 startPosition = lineRenderer.GetPosition(0);
            Vector3 currentPosition = lineRenderer.GetPosition(1);
            Vector3 direction = currentPosition - startPosition;
            Vector3 force = direction * forceAdd;
            rigidbody2D.AddForce(- force,ForceMode2D.Impulse); 
    }
}
