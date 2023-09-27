using UnityEngine;

public class ScoreTriggerController : MonoBehaviour
{
    [SerializeField] private HoopController hoopController;
    private Rigidbody2D _rigidbody2D;
    public void ActivateRotation(GameObject ball)
    {
        Debug.Log("ActivateRotation");
        hoopController.IsRotating = true;
        hoopController.ball = ball.GetComponent<Ball>();
        _rigidbody2D = ball.GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.simulated = false;
        ball.transform.SetParent(hoopController.gameObject.transform);
    }
    public void DeactivateRotation(GameObject ball)
    {
        hoopController.IsRotating = false;
        hoopController.ball = null;
        ball.transform.SetParent(null);
    }
}