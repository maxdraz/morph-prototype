using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Velocity))]
public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1f;
    private Velocity velocity;
    private float gravity => Physics.gravity.y * gravityScale;

    // Start is called before the first frame update
    void Awake()
    {
        velocity = GetComponent<Velocity>();
    }

    private void Update()
    {
        velocity.Add(Vector3.up * gravity * Time.deltaTime);
    }
}
   
