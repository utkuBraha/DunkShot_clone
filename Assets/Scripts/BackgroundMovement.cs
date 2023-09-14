using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lenght = 10f;
    private Vector2 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, lenght);
        transform.position = startPosition + Vector2.down * newPosition;
    }
}
