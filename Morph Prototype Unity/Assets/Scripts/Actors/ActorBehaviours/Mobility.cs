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
    CombatResources combatResources;

    // Start is called before the first frame update
    void Start()
    {
        combatResources = GetComponent<CombatResources>();
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
            combatResources.DelayStaminaRegen();

            if (canDodge)
            {

                if (waitingForSecondInput)
                {
                    if (combatResources.staminaPoints >= 100) 
                    {
                        //double dodge
                        Vector3 inputVector = GetInputRelativeToCamera();
                        Vector3 dodgeVector = (forceToApply * 1.5f) * inputVector;
                        rb.AddForce(dodgeVector, ForceMode.Impulse);
                        Debug.Log("second dodge");
                        waitingForSecondInput = false;
                        canDodge = false;
                        combatResources.SpendStamina(100);
                        combatResources.DelayStaminaRegen();
                        Invoke("DodgeCooldown", dodgeCooldown);
                    }
                }

                else
                {
                    if (combatResources.staminaPoints >= 75) 
                    {
                        //single dodge
                        Vector3 inputVector = GetInputRelativeToCamera();
                        Vector3 dodgeVector = forceToApply * inputVector;
                        rb.AddForce(dodgeVector, ForceMode.Impulse);
                        combatResources.SpendStamina(75);
                        combatResources.DelayStaminaRegen();
                        Debug.Log("first dodge");
                        waitingForSecondInput = true;
                    }
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
                combatResources.DisableStaminaRegen();
                
            }
        }
    }
}
