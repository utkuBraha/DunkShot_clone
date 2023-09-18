using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    private Vector2 _threshold;
    public float _speed;
    public Transform ball;

    public void Update()
    {
        FollowPlayer();
    }
    private Vector2 CalculateThreshold()
    {
        return new Vector2(
            Camera.main.orthographicSize * Camera.main.aspect - _offset.x,
            Camera.main.orthographicSize - _offset.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        _threshold = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(_threshold.x * 2, _threshold.y * 2, 0.5f));
    }
    private void FollowPlayer()
    {
        Vector3 position = transform.position;
        if (Mathf.Abs(transform.position.y - ball.transform.position.y) > _threshold.y)
            position.y = ball.transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
    }
}