using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    //This needs a value assigned to it from the stats script
    public float maxStealth;

    float currentStealth;
    public float finalStealthValue;
    public bool stealthMode;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            if (!stealthMode)
            {
                stealthMode = true;
            }
            else
            {
                stealthMode = false;
            }
        }


        float currentSpeed = rb.velocity.magnitude;
        //Debug.Log(currentSpeed);




        currentStealth = maxStealth / (currentSpeed / 2);


        if (currentStealth > maxStealth * 2)
        {
            currentStealth = maxStealth * 2;
        }

        if (stealthMode)
        {
            currentStealth *= 2;

        }


        if (!stealthMode && currentSpeed == 0)
        {
            currentStealth = maxStealth;
        }

        finalStealthValue = currentStealth;

    }
}
