using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMover : MonoBehaviour
{
    [SerializeField] private Vector3 equilibriumPos;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float amplitude;
    [SerializeField] private Vector3 localAxis = Vector3.up;
    private Vector3 offset;

    private float theta;
    // Start is called before the first frame update
    void Start()
    {
        localAxis.Normalize();
        equilibriumPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteSineMovement();
    }

    void ExecuteSineMovement()
    {
        theta += speed * Time.deltaTime;
        //localAxis = transform.TransformDirection(localAxis);
        offset = localAxis * (Mathf.Sin(theta) * (amplitude*2));
        offset = transform.TransformDirection(offset);
        transform.position = equilibriumPos + offset;

    }
}
