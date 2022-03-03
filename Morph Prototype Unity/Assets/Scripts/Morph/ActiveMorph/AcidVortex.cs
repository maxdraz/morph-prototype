using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortex : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private GameObject acidVortexParticle;
    [SerializeField] private float damage;
    [SerializeField] private float explosionDelay;


    public Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),

    };


    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnAcidVortex();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnAcidVortex();
        }
    }


    private void SpawnAcidVortex()
    {
        GameObject acidVortex = ObjectPooler.Instance.GetOrCreatePooledObject(acidVortexParticle);
        acidVortex.GetComponent<AOE_DELAY>().SetDamageDealer(GetComponent<DamageHandler>());
        acidVortex.transform.parent = transform;
        acidVortex.transform.position = transform.position;

    }
}
