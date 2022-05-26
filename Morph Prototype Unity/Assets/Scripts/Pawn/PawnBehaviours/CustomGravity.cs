using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float gravityScale = 1f;
    private float gravity = Physics.gravity.y;

    public float Gravity => gravityScale;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public float ChangeGravity(float newGravityScale) 
    {
        gravityScale = newGravityScale;

        return gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * gravity * gravityScale, ForceMode.Acceleration);
    }
}
