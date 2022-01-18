using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Velocity))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float bonusMoveSpeed;
    private Velocity velocity;
    private Vector3 movementDirection;

    public float MovementSpeedNormalized => velocity.CurrentHorizontalVelocity.magnitude / speed;
    Stats stats;
    
    void Awake()
    {
        stats = GetComponentInChildren<Stats>(); 
        velocity = GetComponent<Velocity>();
        
    }
 
    private void Update()
    {
        if(movementDirection == Vector3.zero && velocity.CurrentHorizontalVelocity == Vector3.zero) return;
        
        movementDirection *= speed + (speed * bonusMoveSpeed);
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
