using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 velocity;
   

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
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;


        input = Camera.main.transform.TransformDirection(input);
        input = Vector3.ProjectOnPlane(input, Vector3.up).normalized;
        
        if (input.magnitude  > 0.7f)
        {
            if (Input.GetMouseButton(1))
            {
                //strafe
                // camera forward
                var camForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camForward), 0.2f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(input), 0.2f);
            }
            
        }

        velocity = input * speed;
        
        velocity.y = rb.velocity.y;
            
        rb.velocity = velocity;
        
        
        
        //transform.position += input * speed* Time.deltaTime;    

    }
}
