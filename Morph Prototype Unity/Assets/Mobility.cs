using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobility : MonoBehaviour
{
    public Input mobilityInput;

    float currentHoldTime;

    float holdToSprintTime = 1f;
    float timeBetweenPresses;
    float maxTimeBetweenPresses = .3f;
    bool waitingForSecondInput;
    float forceToApply = 10f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waitingForSecondInput = false;
    }

    private Vector3 GetInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            currentHoldTime += Time.deltaTime;

            if (currentHoldTime > holdToSprintTime)
            {
                //start sprinting
            }

            else
            {
                if (waitingForSecondInput)
                {
                    //double dodge
                    Vector3 inputVector = GetInput();
                    Vector3 dodgeVector = (forceToApply * 1.5f) * inputVector;
                    rb.AddForce(dodgeVector, ForceMode.Impulse);
                }

                else
                {
                    //single dodge
                    Vector3 inputVector = GetInput();
                    Vector3 dodgeVector = forceToApply * inputVector;
                    rb.AddForce(dodgeVector, ForceMode.Impulse);
                }
            }
        }

        else 
        {
            //stop sprinting

            currentHoldTime = 0f;

            if (timeBetweenPresses == 0f)
            {
                waitingForSecondInput = true;
            }

            if (waitingForSecondInput)  
            {
                timeBetweenPresses += Time.deltaTime;  
            }

            
            if (timeBetweenPresses > maxTimeBetweenPresses) 
            {
                waitingForSecondInput = false;
            }
        }
    }
}
