using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float maxSpeed = 10f;
    private float minSpeed = 2f;
    [SerializeField] private float speed;
    public float speedModifier = 1;
    [Range(0,1)]
    [SerializeField] private float rotationStrength;
    private Vector3 velocity;
    private Vector3 input;
    private Rigidbody rb;
    private bool sprinting;

    private CreatureVirtualController controller;
    private Vector3 moveDir;

    private Transform lookRotationTransform;

    private void Reset()
    {
        speed = 5f;
        sprinting = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CreatureVirtualController>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerCreatureCharacter.Instance.CanAcceptInput) return;
        
        input = GetInputRelativeToCamera();
        
        UpdateVelocity();
        UpdateRotation();

        if (Input.GetKeyDown("up") && speed < maxSpeed) 
        {
            speed += 1f;
            Debug.Log("Speed set to " + speed);
        }
        if (Input.GetKeyDown("down") && speed > minSpeed)
        {
            speed -= 1f;
            Debug.Log("Speed set to " + speed);
        }
    }

    void UpdateRotation()
    {
        // if look rotation transform
        if (lookRotationTransform)
        {
            // look in direactio
            var lookDirection = UtilityFunctions.TransformForwardOnPlane(lookRotationTransform, Vector3.up);
            LookInDirection(lookDirection, in rotationStrength);
            return;
        }

        if (InputGreaterThan(0.7f) && !IsStrafing())
        {
            LookInDirection(in input, in rotationStrength);
        }
        // else if(IsStrafing())
        // {
        //     LookInDirection(UtilityFunctions.CameraForwardOnPlane(Vector3.up), in rotationStrength);
        // }
    }

    public float AdjustSpeedModifier(float amountToAdjustBy) 
    {
        speedModifier += amountToAdjustBy;
        return speedModifier;
    }

    private bool IsStrafing()
    {
        return Input.GetMouseButton(1);
    }

    private bool InputGreaterThan(float magnitude)
    {
        return input.magnitude > magnitude;
    }

    public void StartSprinting()
    {
        sprinting = true;
        //Debug.Log("stopped sprinting"); 
    }

    public void StopSprinting()
    {
        sprinting = false;
        //Debug.Log("stopped sprinting");
    }

    private void UpdateVelocity()
    {
        if (sprinting)
        {
            velocity = (input * (speed * 2) * (1 + speedModifier));
        }
        else 
        {
            velocity = (input * speed * (1 + speedModifier));
        }

        
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private Vector3 GetInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    private void ClampMagnitude(ref Vector3 vec, float min, float max)
    {
        if (vec.magnitude >= max)
        {
            vec = vec.normalized * max;
        } else if (vec.magnitude <= 0)
        {
            vec = Vector3.zero;
        }
    }

    private Vector3 GetInputRelativeToCamera()
    {
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        moveDir = Vector3.ProjectOnPlane(moveDir, Vector3.up).normalized;
        return moveDir;
    }

    private void LookInDirection(Vector3 direction, in float slerpStep)
    {
        LookInDirection(in direction, in slerpStep);
    }
    
    private void LookInDirection(in Vector3 direction, in float slerpStep)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), slerpStep);
    }

    public float GetMoveSpeedNormalized()
    {
        var currentVel = rb.velocity;
        var velocityXZ = new Vector3(currentVel.x, 0, currentVel.z);
        return Mathf.Min(velocityXZ.magnitude / maxSpeed, 1);
    }

    public void SetMovementDirection(Vector2 dir)
    {
        moveDir = new Vector3(dir.x, 0, dir.y);
        moveDir =  Vector3.ClampMagnitude(moveDir, 1);
    }

    public void SetLookRotationTransform(Transform trans)
    {
        lookRotationTransform = trans;
    }

    public void ClearLookRotationTransform()
    {
        lookRotationTransform = null;
    }
}
