using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftingLeap : ActiveMorph
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    private Rigidbody rb;
    private bool goingToImpact;
    private Vector3 landingParticlesOffset = new Vector3(0, -1, 0);
    [SerializeField] private GameObject impactParticles;

    [SerializeField] private float physicalDamage;
    [SerializeField] private float knockUpForce;
    [SerializeField] private float range;

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
        if (rb == null) 
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        rb.AddForce(0, 1 * verticalForce, 0, ForceMode.Impulse);

        rb.AddForce(0, 0, 1 * horizontalForce, ForceMode.Impulse);

        Debug.Log("Leaping");

        goingToImpact = true;
    }

    private void Impact() 
    {
        GameObject landingParticles = ObjectPooler.Instance.GetOrCreatePooledObject(impactParticles);
        landingParticles.transform.position = transform.position + landingParticlesOffset;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() && hitCollider.gameObject != gameObject)
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PhysicalDamageData (physicalDamage),damageHandler);
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new KnockupData(knockUpForce), damageHandler);
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
