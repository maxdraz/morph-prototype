using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterCreature : ActiveMorph
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    Rigidbody rb;
    bool goingToImpact;
    
    [SerializeField] private float physicalDamage;
    [SerializeField] private float range;
    public GameObject crater;

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

    protected override void Update()
    {
        base.Update();
        
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
        Instantiate(crater, transform.position, transform.rotation);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData(physicalDamage), damageHandler);
                
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
