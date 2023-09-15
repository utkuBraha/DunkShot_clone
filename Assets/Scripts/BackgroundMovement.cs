using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lenght = 10f;
    private Vector2 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, lenght);
        transform.position = _startPosition + Vector2.down * newPosition;
    }
}
