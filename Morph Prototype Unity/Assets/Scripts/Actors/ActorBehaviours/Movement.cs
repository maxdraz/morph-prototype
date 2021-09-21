using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float maxSpeed = 10f;
    private float minSpeed = 2f;
    [SerializeField] private float speed;
    [Range(0,1)]
    [SerializeField] private float rotationStrength;
    private Vector3 velocity;
    private Vector3 input;
    private Rigidbody rb;

    private void Reset()
    {
        speed = 5f;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = GetInputRelativeToCamera();
        
        UpdateVelocity();
        UpdateRotation();

        if (Input.GetKeyDown("up") && speed<maxSpeed) 
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
        if (InputGreaterThan(0.7f) && !IsStrafing())
        {
            LookInDirection(in input, in rotationStrength);
        }
        else if(IsStrafing())
        {
            LookInDirection(UtilityFunctions.CameraForwardOnPlane(Vector3.up), in rotationStrength);
        }
    }

    private bool IsStrafing()
    {
        return Input.GetMouseButton(1);
    }

    private bool InputGreaterThan(float magnitude)
    {
        return input.magnitude > magnitude;
    }

    private void UpdateVelocity()
    {
        velocity = input * speed;
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
        var inpt = GetInput();
        inpt  = Camera.main.transform.TransformDirection(inpt);
        inpt = Vector3.ProjectOnPlane(inpt, Vector3.up).normalized;
        return inpt;
    }

    private void LookInDirection(Vector3 direction, in float slerpStep)
    {
        LookInDirection(in direction, in slerpStep);
    }
    
    private void LookInDirection(in Vector3 direction, in float slerpStep)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), slerpStep);
    }
}
