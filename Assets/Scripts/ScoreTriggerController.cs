using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerController : MonoBehaviour
{
    [SerializeField] private HoopController hoopController;
    
    public void ActivateRotation()
    {
        Debug.Log("Activate Rotation");
        hoopController.IsRotating = true;
    }
    public void DeactivateRotation()
    {
        hoopController.IsRotating = false;
    }
    
}
