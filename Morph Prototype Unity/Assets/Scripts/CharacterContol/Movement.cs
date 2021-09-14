using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
