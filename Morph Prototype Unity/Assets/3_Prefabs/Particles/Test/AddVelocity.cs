using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocity : MonoBehaviour
{
    public float forceToApply;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 velocityVector = Vector3.forward * forceToApply;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(velocityVector, ForceMode.Impulse);
    }
}
