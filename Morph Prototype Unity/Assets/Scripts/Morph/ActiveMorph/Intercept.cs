using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : ActiveMorph
{
    static int agilityPrerequisit = 200;


    DamageHandler damageHandler;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;
    Stamina stamina;
    [SerializeField] private float staminaGain;
    bool gainedStamina;
    [SerializeField] private float range;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("agility", agilityPrerequisit),
    };

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        rb = GetComponentInParent<Rigidbody>();
        stamina = GetComponent<Stamina>();
        goingToImpact = false;
    }



    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void FixedUpdate()
    {
        if (cooldown.JustCompleted) 
        {
            gainedStamina = false;
        }

        if (Input.GetKeyDown(testInput)) 
        {
            Leap();
        }
    }

    private void Leap()
    {
        rb.AddForce(Vector3.up, ForceMode.Impulse);

        rb.AddForce(Vector3.forward, ForceMode.Impulse);

        goingToImpact = true;
    }

    private void Impact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                stamina.AddStamina(staminaGain);
                gainedStamina = true;
            }
        }

        goingToImpact = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (goingToImpact)
            {
                Impact();
            }
        }
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

        }

        damageHandler = null;
    }
}
