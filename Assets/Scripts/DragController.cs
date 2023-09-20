// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class DragController : MonoBehaviour
// {
//     public LineRenderer lineRenderer;
//     public Rigidbody2D rb;
//     public float forceAdd = 10f;
//     private Camera _mainCamera;
//     private bool _isDragging;
//     private Vector2 _dragStartPos;
//     public GameObject ball;
//     
//     private List<Vector3> dragPath;
//   
//
//     private void Start()
//     {
//         lineRenderer.positionCount = 0;
//         _mainCamera = Camera.main;
//         
//         dragPath = new List<Vector3>();
//     }
//     private void Update()
//     {
//         if (rb.velocity.magnitude <= 0.1f)
//         {
//             if (Input.GetMouseButtonDown(0) && !_isDragging)
//             {
//                 StartDrag();
//             }
//             if (_isDragging)
//             {
//                 ContinueDrag();
//             }
//             if (Input.GetMouseButtonUp(0) && _isDragging)
//             {
//                 EndDrag();
//             }
//         }
//     }
//     private void StartDrag()
//     {
//         _isDragging = true;
//         _dragStartPos = ball.transform.position;
//         lineRenderer.positionCount = 1;
//         lineRenderer.SetPosition(0, _dragStartPos);
//     }
//     private void ContinueDrag()
//     {
//         Vector2 currentPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
//         Vector2 direction = _dragStartPos - currentPosition;
//         int numberOfPoints = 100; 
//         Vector3[] positions = new Vector3[numberOfPoints];
//         
//         dragPath.Clear();
//         for (int i = 0; i < numberOfPoints; i++)
//         {
//             float t = i / (float)(numberOfPoints - 1);
//             float x = _dragStartPos.x + direction.x * t;
//             float y = _dragStartPos.y + direction.y * t + 0.5f * Physics2D.gravity.y * t * t;
//             positions[i] = new Vector3(x, y, 0);
//             dragPath.Add(positions[i]);
//         }
//         lineRenderer.positionCount = numberOfPoints;
//         lineRenderer.SetPositions(positions);
//     }
//     private void EndDrag()
//     {
//         _isDragging = false;
//         lineRenderer.positionCount = 0;
//         Vector2 endPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
//         Vector2 direction = _dragStartPos - endPos;
//         Vector2 force = direction.normalized * forceAdd;
//         rb.velocity = Vector2.zero;
//         rb.AddForce(force, ForceMode2D.Impulse);
//         StartCoroutine(FollowPath());
//
//     }
//     private IEnumerator FollowPath()
//     {
//         float speed = forceAdd;
//         float distance = 0f;
//
//         for (int i = 1; i < dragPath.Count; i++)
//         {
//             Vector3 startPoint = dragPath[i - 1];
//             Vector3 endPoint = dragPath[i];
//             float segmentDistance = Vector3.Distance(startPoint, endPoint);
//
//             while (distance < segmentDistance)
//             {
//                 distance += speed * Time.deltaTime;
//                 float t = distance / segmentDistance;
//                 ball.transform.position = Vector3.Lerp(startPoint, endPoint, t);
//                 yield return null;
//             }
//
//             distance -= segmentDistance;
//         }
//     }
//
//     
//     private void OnMouseUp()
//     {
//         rb.velocity = Vector2.zero;
//     }
// }


using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb;
    public float forceAdd = 10f;
    public float minDragDistance = 0.1f; 
    public float maxDragDistance = 1.0f; 
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector2 _dragStartPos;
    public GameObject ball;

    private void Start()
    {
        lineRenderer.positionCount = 0;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            if (Input.GetMouseButtonDown(0) && !_isDragging)
            {
                StartDrag();
            }
            if (_isDragging)
            {
                ContinueDrag();
            }
            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                EndDrag();
            }
        }
    }

    private void StartDrag()
    {
        _isDragging = true;
        _dragStartPos = ball.transform.position;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, _dragStartPos);
    }

    private void ContinueDrag()
    {
        Vector2 currentPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = _dragStartPos - currentPosition;
        float dragDistance = Mathf.Clamp(direction.magnitude, minDragDistance, maxDragDistance);
        int numberOfPoints = 100;
        Vector3[] positions = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = _dragStartPos.x + direction.x * t * dragDistance;
            float y = _dragStartPos.y + direction.y * t * dragDistance + 0.5f * Physics2D.gravity.y * t * t;
            positions[i] = new Vector3(x, y, 0);
        }
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(positions);
    }

    private void EndDrag()
    {
        _isDragging = false;
        lineRenderer.positionCount = 0;
        Vector2 endPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = _dragStartPos - endPos;
        Vector2 force = direction.normalized * forceAdd;
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    
    private void OnMouseUp()
    {
        rb.velocity = Vector2.zero;
    }
}
