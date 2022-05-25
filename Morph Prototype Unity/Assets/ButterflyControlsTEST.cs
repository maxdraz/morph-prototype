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
    [SerializeField] private bool gliding;
    float glideDelay = .3f;
    public Timer glideTimer = new Timer();

    [SerializeField] private float flightStaminaCost;
    [SerializeField] private float upwardThrust;
    [SerializeField] private float maxUpwardThrust;
    [SerializeField] private bool flapCooldown;
    [SerializeField] private float flapCooldownTime;

    [SerializeField] private float staminaRegenRate;
    [SerializeField] private bool staminaRegenCoolDown;
    bool grounded;

    Vector3 localVel = new Vector3();
    float verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<CustomGravity>();
        anim = GetComponent<Animator>();

        //Debug.Log("");

        glideTimer = new Timer(glideDelay);

        gliding = false;
        flapCooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && flapCooldown)
        {
            //Debug.Log("Space Bar Pressed");
            Flap();
        }

        if (Input.GetButton("Jump") && !flapCooldown)
        {
            //Debug.Log("Space Bar Pressed");
            gliding = true;
        }

        if (Input.GetButton("Jump") == false)
        {
            //Debug.Log("Space Bar Released");
            gliding = false;
        }

        if (Input.GetAxis("Vertical") < 0)
        {

        }
        else
        {

        }

        if (Input.GetAxis("Horizontal") < 0)
        {

        }
        else
        {

        }

        if (gliding)
        {
            //Debug.Log("Holding space");

            glideTimer.Update(Time.deltaTime);

            if (glideTimer.JustCompleted) 
            {
                Debug.Log("Starting to glide");
                gravity.ChangeGravity(glideGravity);
            }
        }
        if (gliding && Input.GetButton("Jump") == false)
        {
            //Debug.Log("Stopped gliding");
            gliding = false;
            gravity.ChangeGravity(1);
            glideTimer = new Timer(glideDelay);
        }

        verticalVelocity = rb.velocity.y;
        localVel = transform.InverseTransformDirection(rb.velocity);

    }

   //void Glide(bool gliding) 
   //{
   //    if (gliding)
   //    {
   //        gravity.ChangeGravity(glideGravity);
   //    }
   //    else
   //    {
   //        gravity.ChangeGravity(1);
   //    }
   //}

    void Flap()
    {
        float upwardThrustBoost;

        stamina.SubtractStamina(flightStaminaCost);

        Debug.Log("velocity.y = " + localVel.y);

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

        Debug.Log("upwardThrust" + " = " + upwardThrust);
        anim.SetTrigger("Flap");
        StartCoroutine("FlapCooldown");
    }

    IEnumerator FlapCooldown() 
    {
        yield return new WaitForSeconds(flapCooldownTime);

        flapCooldown = true;

        yield return null;
    }

    void RegenStamina() 
    {
        if (grounded && staminaRegenCoolDown) 
        {
            stamina.AddStamina(staminaRegenRate);
        }
    }

    IEnumerator RegenCooldown() 
    {
        yield return new WaitForSeconds(staminaRegenRate);

        staminaRegenCoolDown = true;

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
        {
            StartCoroutine("RegenCooldown");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
            StopCoroutine("RegenStamina");
        }
    }
}
