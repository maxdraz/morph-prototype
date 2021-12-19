using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LegacyJump : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float jumpForce = 100f;
    [Range(1,5)]
    [SerializeField] private int maxNumberOfJumps = 3;

    private int jumpsLeft;
    [SerializeField] private LayerMask isGround; 
    private Collider collider;
    [Range(0.1f, 3)]
    [SerializeField] private float raycastLength = 0.3f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpsLeft = maxNumberOfJumps;
    }

    private void Start()
    {
        collider = GetComponentInChildren<Collider>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteJump();
        }
    }

    private void ExecuteJump()
    {
        bool isGrounded = IsGrounded();
        if(isGrounded) jumpsLeft = maxNumberOfJumps;
        
        if(IsGrounded() || jumpsLeft > 0)
        {
            jumpsLeft--;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        
    }

    private bool IsGrounded()
    {
        List<Ray> rays = new List<Ray>();
        Ray bottomLeft = new Ray(new Vector3(collider.bounds.min.x, collider.bounds.min.y +  0.1f, collider.bounds.min.z),
            Vector3.down);
        Ray topLeft = new Ray(new Vector3(collider.bounds.min.x, collider.bounds.min.y+  0.1f, collider.bounds.max.z),
            Vector3.down);
        Ray bottomRight = new Ray(new Vector3(collider.bounds.max.x, collider.bounds.min.y+  0.1f, collider.bounds.min.z),
            Vector3.down);
        Ray topRight = new Ray(new Vector3(collider.bounds.max.x, collider.bounds.min.y+  0.1f, collider.bounds.max.z),
            Vector3.down);
        
        rays.Add(bottomLeft);
        rays.Add(topLeft);
        rays.Add(bottomRight);
        rays.Add(topRight);

        bool isGrounded = false;

        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, raycastLength, isGround))
            {
                isGrounded = true;
            }
        }
        return isGrounded;
    }
}
