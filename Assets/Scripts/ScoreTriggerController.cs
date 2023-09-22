using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerController : MonoBehaviour
{
    [SerializeField] private HoopController hoopController;
    private Rigidbody2D _rigidbody2D;
    public void ActivateRotation(GameObject ball)
    {
        hoopController.IsRotating = true;
        _rigidbody2D = ball.GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.simulated = false;
        ball.transform.SetParent(hoopController.gameObject.transform);
        
    }
    public void DeactivateRotation(GameObject ball)
    {
       
        hoopController.IsRotating = false;
        ball.transform.SetParent(null);
    }
    
}
