using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Velocity))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    public float bonusPercentMoveSpeed;
    private Velocity velocity;
    private Vector3 movementDirection;


    public float MovementSpeedNormalized => velocity.CurrentHorizontalVelocity.magnitude / baseSpeed;
    Stats stats;
    
    void Awake()
    {
        stats = GetComponentInChildren<Stats>(); 
        velocity = GetComponent<Velocity>();
        baseSpeed = stats ? stats.BaseMoveSpeed : 100;

    }

    private void Update()
    {
        if(movementDirection == Vector3.zero && velocity.CurrentHorizontalVelocity == Vector3.zero) return;
        
        movementDirection *= baseSpeed + (baseSpeed * bonusPercentMoveSpeed);
        velocity.SetHorizontalVelocity(movementDirection.x, movementDirection.z);
    }

    public void Move(Vector2 horizontalAndVerticalAxis)
    {
        var input = new Vector3(horizontalAndVerticalAxis.x, 0, horizontalAndVerticalAxis.y);
        if (input == Vector3.zero)
        {
            movementDirection = Vector3.zero;
        }

        input = Vector3.ClampMagnitude(input, 1f);
        
        movementDirection =  Vector3.ProjectOnPlane(Camera.main.transform.TransformDirection(input), Vector3.up).normalized * input.magnitude;
        
    }
    
    public void Move(Vector2 horizontalAndVerticalAxis, Transform relativeTo)
    {
        var input = new Vector3(horizontalAndVerticalAxis.x, 0, horizontalAndVerticalAxis.y);
        input = Vector3.ClampMagnitude(input, 1f);

        movementDirection = Vector3.ProjectOnPlane(relativeTo.TransformDirection(input), Vector3.up).normalized * input.magnitude;
    }
}
