using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Rigidbody))]
public class Velocity : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    private Rigidbody rb;
    
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;
    public Vector3 CurrentVelocity => velocity;
    public Vector3 CurrentHorizontalVelocity => new Vector3(velocity.x,0,velocity.z);
    
    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(velocity * Time.deltaTime);
        if (characterController.isGrounded && velocity.y < 0)
        {
            SetY(0);
        }
    }
    
    public void Add(Vector3 velocityToAdd)
    {
        velocity += velocityToAdd;
    }
    public void SetHorizontalVelocity(float x, float z)
    {
        velocity.x = x;
        velocity.z = z;
    }

    public void SetX(float x)
    {
        velocity.x = x;
    }
    public void SetY(float y)
    {
        velocity.y = y;
    }
    public void SetZ(float z)
    {
        velocity.z = z;
    }
}
