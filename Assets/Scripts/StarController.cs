using UnityEngine;

public class StarController : MonoBehaviour
{
    public event System.Action OnStarCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnStarCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
