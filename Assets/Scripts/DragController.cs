using System.Collections;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb;
    //public float dragLimit = 5f;
    public float forceAdd = 10f;
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector2 _dragStartPos;

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
        _dragStartPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, _dragStartPos);
    }

    private void ContinueDrag()
    {
        Vector2 currentPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = currentPosition - _dragStartPos;
        int numberOfPoints = 10; 
        Vector3[] positions = new Vector3[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = _dragStartPos.x + direction.x * t;
            float y = _dragStartPos.y + direction.y * t + 0.5f * Physics2D.gravity.y * t * t;
            positions[i] = new Vector3(x, y, 0);
        }
        // y vektorunu - ile carip yonunu tersine cevirebiliriz
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(positions);
    }

    private void EndDrag()
    {
        _isDragging = false;
        lineRenderer.positionCount = 0;

        Vector2 endPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = endPos - _dragStartPos;
        Vector2 force = direction.normalized * forceAdd;
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnMouseUp()
    {
        rb.velocity = Vector2.zero;
    }
}
