// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class DragController : MonoBehaviour
// {
//     public LineRenderer lineRenderer;
//     public Rigidbody2D rigidbody2D;
//     public float draglimit = 5f;
//     public float forceAdd = 10f;
//     private Camera mainCamera;
//     private bool isDragging;
//    
//     public LineRenderer pathRenderer; 
//     public float pathSegmentLength = 0.1f; 
//     
//     
//
//     private void Start()
//     {
//         lineRenderer.positionCount = 2;
//         lineRenderer.SetPosition(0, Vector2.zero);
//         lineRenderer.SetPosition(1, Vector2.zero);
//         lineRenderer.enabled = false;
//         mainCamera = Camera.main;
//         pathRenderer.positionCount = 0;
//         
//     }
//
//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0) && !isDragging)
//         {
//             DragStart();
//         }
//         if (isDragging)
//         {
//             Drag();
//         }
//         if (Input.GetMouseButtonUp(0) && isDragging)
//         {
//             DragEnd();
//         }
//     }
//     Vector3 GetMousePosition() 
//     {
//         Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
//         mousePosition.z = 0f;
//         return mousePosition;
//     }
//     void DragStart()
//     {
//         lineRenderer.enabled = true;
//         isDragging = true;
//         lineRenderer.SetPosition(0,GetMousePosition());
//         pathRenderer.positionCount = 1;
//         pathRenderer.SetPosition(0, transform.position); 
//     }
//
//     // public void UpdateOrbit(Vector3 forceVector,Vector3 startingPosition)
//     // {
//     //     Vector3 velocity = (forceVector / rigidbody2D.mass) * Time.fixedDeltaTime;
//     //     float FlightDuration = (2 * velocity.y) / Physics2D.gravity.y;
//     //     float stepSize = FlightDuration / lineCount;
//     //     linePoints.Clear();
//     //     for (int i = 0; i < lineCount; i++)
//     //     {
//     //         float stepTime = stepSize * i;
//     //         Vector3 movementVector = new Vector3(velocity.x * stepTime, velocity.y * stepTime + 0.5f * Physics2D.gravity.y * stepTime * stepTime, 0);
//     //         linePoints.Add(startingPosition + -movementVector);
//     //     }
//     //     lineRenderer.positionCount = linePoints.Count;
//     //     lineRenderer.SetPositions(linePoints.ToArray());
//     // }
//     
//
//     void Drag()
//     {
//         Vector3 startPosition = lineRenderer.GetPosition(0);
//         Vector3 currentPosition = GetMousePosition();
//         Vector3 direction = currentPosition - startPosition;
//         if (direction.magnitude <= draglimit)
//         {
//             lineRenderer.SetPosition(1, currentPosition);
//         }
//         else
//         {
//             Vector3 dragPosition = startPosition + direction.normalized * draglimit;
//             lineRenderer.SetPosition(1,dragPosition);
//         }
//         if (Vector3.Distance(pathRenderer.GetPosition(pathRenderer.positionCount - 1), currentPosition) >= pathSegmentLength)
//         {
//             pathRenderer.positionCount++;
//             pathRenderer.SetPosition(pathRenderer.positionCount - 1, currentPosition);
//         }
//     }
//     private void DragEnd()
//     {
//         isDragging = false;
//         lineRenderer.enabled = false;
//         Vector3 startPosition = lineRenderer.GetPosition(0);
//         Vector3 currentPosition = lineRenderer.GetPosition(1);
//         Vector3 direction = currentPosition - startPosition;
//         Vector3 force = direction * forceAdd;
//         rigidbody2D.AddForce(- force,ForceMode2D.Impulse); 
//         pathRenderer.positionCount = 0;
//     }
// }

using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rigidbody2D;
    public float dragLimit = 5f;
    public float forceAdd = 10f;
    private Camera mainCamera;
    private bool isDragging;
    private Vector2 dragStartPos;

    private void Start()
    {
        lineRenderer.positionCount = 0;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            StartDrag();
        }
        if (isDragging)
        {
            ContinueDrag();
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }
    }

    private void StartDrag()
    {
        isDragging = true;
        dragStartPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, dragStartPos);
    }

    private void ContinueDrag()
    {
        Vector2 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = currentPosition - dragStartPos;
        int numberOfPoints = 100; 
        Vector3[] positions = new Vector3[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = dragStartPos.x + direction.x * t;
            float y = dragStartPos.y + direction.y * t + 0.5f * Physics2D.gravity.y * t * t;
            positions[i] = new Vector3(x, y, 0);
        }
        // y vektorunu - ile carip yonunu tersine cevirebiliriz
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(positions);
    }

    private void EndDrag()
    {
        isDragging = false;
        lineRenderer.positionCount = 0;

        Vector2 endPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = endPos - dragStartPos;
        Vector2 force = direction.normalized * forceAdd;

        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }
}
