using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftingLeap : ActiveMorph
{
    static int agilityDamagePrerequisite;
    static int toughnessPrerequisite;


    DamageHandler damageHandler;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;


    [SerializeField] private float physicalDamage;
    [SerializeField] private float knockUpForce;
    [SerializeField] private float range;

    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("agility", agilityDamagePrerequisite),
        new Prerequisite("toughness", toughnessPrerequisite),
    };

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        rb = GetComponentInParent<Rigidbody>();
        goingToImpact = false;
    }



    private void OnDisable()
    {

        UnsubscribeFromEvents();


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
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData (physicalDamage),damageHandler);
                //hitCollider.GetComponent<DamageHandler>().ApplyDamage(new KnockUp(knockUpForce), damageHandler);
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
