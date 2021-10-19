using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobility : MonoBehaviour
{

    float currentHoldTime;

    float holdToSprintTime = 1f;
    float timeBetweenPresses;
    float maxTimeBetweenPresses = .5f;
    bool waitingForSecondInput;
    float forceToApply = 100f;
    float dodgeCooldown = 1f;
    public bool canDodge;


    Rigidbody rb;
    Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
        waitingForSecondInput = false;
        canDodge = true;
    }

    private Vector3 GetInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    private Vector3 GetInputRelativeToCamera()
    {
        var inpt = GetInput();
        inpt = Camera.main.transform.TransformDirection(inpt);
        inpt = Vector3.ProjectOnPlane(inpt, Vector3.up).normalized;
        return inpt;
    }

    void DodgeCooldown() 
    {
        canDodge = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForSecondInput)
        {
            timeBetweenPresses += Time.deltaTime;
        }

        if (timeBetweenPresses > maxTimeBetweenPresses)
        {
            waitingForSecondInput = false;
            timeBetweenPresses = 0f;
            canDodge = false;
            Invoke("DodgeCooldown", dodgeCooldown);
        }

        if (Input.GetKeyUp("space"))
        {

            //stop sprinting
            movement.StopSprinting();

            if (canDodge)
            {

                if (waitingForSecondInput)
                {
                    //double dodge
                    Vector3 inputVector = GetInputRelativeToCamera();
                    Vector3 dodgeVector = (forceToApply * 1.5f) * inputVector;
                    rb.AddForce(dodgeVector, ForceMode.Impulse);
                    Debug.Log("second dodge");
                    waitingForSecondInput = false;
                    canDodge = false;
                    Invoke("DodgeCooldown", dodgeCooldown);
                }

                else
                {
                    //single dodge
                    Vector3 inputVector = GetInputRelativeToCamera();
                    Vector3 dodgeVector = forceToApply * inputVector;
                    rb.AddForce(dodgeVector, ForceMode.Impulse);
                    Debug.Log("first dodge");
                    waitingForSecondInput = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime > holdToSprintTime)
            {
                //start sprinting
                movement.StartSprinting();
            }
        }
    }
}
