using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyControlsTEST : MonoBehaviour
{
    Stamina stamina;
    Rigidbody rb;
    CustomGravity gravity;
    Animator anim;

    [SerializeField] private float glideGravity;
    bool gliding;
    float startingGravity;

    [SerializeField] private float flightStaminaCost;
    [SerializeField] private float baseUpwardThrust;
    [SerializeField] private float maxUpwardThrust;
    [SerializeField] private bool flapCooldown;
    [SerializeField] private float flapCooldownTime;

    public bool grounded;

    [SerializeField] private float yawforce;
    [SerializeField] private float verticalDiveForce;
    [SerializeField] private float forwardDiveForce;
    [SerializeField] private float reverseForce;


    Vector3 localVel = new Vector3();
    float verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<CustomGravity>();
        anim = GetComponent<Animator>();

        startingGravity = gravity.Gravity;
        //Debug.Log("");

        gliding = false;
        flapCooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        float yaw = Input.GetAxis("Horizontal");
        float pitch = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && flapCooldown)
        {
            //Debug.Log("Space Bar Pressed");
            Flap();
            StartCoroutine("Glide");
        }

        if (pitch < 0)
        {
            rb.AddForce(-transform.up * verticalDiveForce, ForceMode.Force);
            rb.AddForce(transform.forward * forwardDiveForce, ForceMode.Force);
        }
        if (pitch > 0)
        {
            rb.AddForce(-transform.forward * reverseForce, ForceMode.Force);
        }


        rb.AddTorque(transform.up * yawforce * yaw, ForceMode.Force);




        if (gliding && Input.GetButton("Jump") == false)
        {
            Debug.Log("Stopped gliding");
            gravity.ChangeGravity(startingGravity);
            gliding = false;
            anim.SetBool("Gliding", false);
        }

        verticalVelocity = rb.velocity.y;
        localVel = transform.InverseTransformDirection(rb.velocity);

        if (Input.GetButton("Jump") == false)
        {
            StopCoroutine("Glide");
        }
    }

    IEnumerator Glide() 
    {
        //Debug.Log("Started gliding");
        
        yield return new WaitForSeconds(.2f);
        anim.SetBool("Gliding", true);
        //Debug.Log("Gravity changed");

        gliding = true;
        gravity.ChangeGravity(startingGravity * glideGravity);

        yield return null;
        
    }

    void Flap()
    {
        if (stamina.currentStamina >= flightStaminaCost) {
            float upwardThrustBoost;
            float upwardThrust = baseUpwardThrust;

            stamina.SubtractStamina(flightStaminaCost);

            //Debug.Log("velocity.y = " + localVel.y);

            if (localVel.y < 0)
            {
                upwardThrustBoost = localVel.y * localVel.y / 10;
            }
            else
            {
                upwardThrustBoost = 0;
            }


            upwardThrust += upwardThrustBoost;

            if (upwardThrust > maxUpwardThrust)
            {
                upwardThrust = maxUpwardThrust;
            }



            rb.AddForce(0, upwardThrust, 0, ForceMode.Impulse);

            upwardThrustBoost = 0;

            flapCooldown = false;

            //Debug.Log("upwardThrust" + " = " + upwardThrust);
            anim.SetTrigger("Flap");
            StartCoroutine("FlapCooldown");
        }
    }

    IEnumerator FlapCooldown() 
    {
        yield return new WaitForSeconds(flapCooldownTime);

        flapCooldown = true;

        yield return null;
    }


    
}
