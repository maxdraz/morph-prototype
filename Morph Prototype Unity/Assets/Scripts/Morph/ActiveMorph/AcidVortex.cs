using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortex : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private GameObject acidVortexParticle;
    [SerializeField] private float damage;
    [SerializeField] private float explosionDelay;
    DamageHandler damageHandler;


    public Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),

    };


    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnAcidVortex();
            Invoke("AcidVortexDamage", explosionDelay);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnAcidVortex();
            Invoke("AcidVortexDamage", explosionDelay);
        }
    }


    private void SpawnAcidVortex()
    {
        GameObject chemicalCocktail = Instantiate(acidVortexParticle, transform.position, transform.rotation);
        chemicalCocktail.transform.parent = this.gameObject.transform;

    }

    private void AcidVortexDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                if (hitCollider.GetComponentInParent<Velocity>().CurrentVelocity.y > 3)
                {
                    hitCollider.GetComponent<DamageHandler>().ApplyDamage(new AcidDamageData(damage * 2), damageHandler);
                }
                else 
                {
                    hitCollider.GetComponent<DamageHandler>().ApplyDamage(new AcidDamageData(damage), damageHandler);

                }
            }
        }
    }
}
