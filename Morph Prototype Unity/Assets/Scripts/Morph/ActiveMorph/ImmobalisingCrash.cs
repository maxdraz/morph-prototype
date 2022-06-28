using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobalisingCrash : ActiveMorph
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    private Rigidbody rb;
    private bool goingToImpact;
    
    [SerializeField] private float physicalDamage;
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private float rootDuration;
    [SerializeField] private float range;
    

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        rb = GetComponentInParent<Rigidbody>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        goingToImpact = false;
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
                //If this is used as a stealth attack the fortitude damage is increased by x 1.5 and the duration oof the root effect is doubled if they fail
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData(physicalDamage), damageHandler);
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new FortitudeDamageData(fortitudeDamage), damageHandler);
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
}
