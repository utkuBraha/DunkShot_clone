using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private float cameraDistance = 3f;
    private Vector2 _threshold;
    public float speed;
    public Transform ball;
    public void Update()
    {
        FollowPlayer();
    }
    private Vector2 CalculateThreshold()
    {
        return new Vector2(
            Camera.main.orthographicSize * Camera.main.aspect - offset.x,
            Camera.main.orthographicSize - offset.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        _threshold = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(_threshold.x * 2, _threshold.y * 2, 0.5f));
    }
    private void FollowPlayer()
    {
        if (ball == null)
        {
            return;
        }
        Vector3 position = Camera.main.transform.position;
        if (Mathf.Abs(Camera.main.transform.position.y - ball.transform.position.y -cameraDistance /2) > _threshold.y) 
            position.y = ball.transform.position.y + cameraDistance /2;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, position, speed * Time.deltaTime);
    }
}