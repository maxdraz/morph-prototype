using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Velocity))]
public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 20f;
    [Range(1,5)]
    [SerializeField] private int maxJumps;
    
    private Velocity velocity;
    private float jumpsLeft;
    // Start is called before the first frame update
    void Awake()
    {
       velocity = GetComponent<Velocity>();
    }

    private void Update()
    {
        if (velocity.CharacterController.isGrounded)
        {
            jumpsLeft = maxJumps;
        }
    }

    public void ExecuteJump()
    {
        if (jumpsLeft > 0)
        {
            velocity.SetY(0);
            velocity.Add(Vector3.up * jumpSpeed);
            jumpsLeft--;
        }
    }
}
