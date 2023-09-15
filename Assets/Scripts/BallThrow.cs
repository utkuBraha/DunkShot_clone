using UnityEngine;

// public class BallThrow : MonoBehaviour
// {
//     private bool surukleme = false;
//     private Vector3 baslangicPozisyonu;
//     private LineRenderer lineRenderer;
//
//     void Start()
//     {
//         lineRenderer = GetComponent<LineRenderer>();
//         lineRenderer.positionCount = 0; 
//     }
//
//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             Vector2 dokunmaPozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             Collider2D hitCollider = Physics2D.OverlapPoint(dokunmaPozisyonu);
//
//             if (hitCollider != null && hitCollider.gameObject == gameObject)
//             {
//                 surukleme = true;
//                 baslangicPozisyonu = transform.position;
//                 lineRenderer.positionCount = 0; 
//             }
//         }
//
//         if (surukleme)
//         {
//             Vector2 dokunmaPozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             transform.position = new Vector3(dokunmaPozisyonu.x, dokunmaPozisyonu.y, transform.position.z);
//
//             lineRenderer.positionCount++;
//             lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
//         }
//
//         if (Input.GetMouseButtonUp(0) && surukleme)
//         {
//             surukleme = false;
//         }
//     }
// }