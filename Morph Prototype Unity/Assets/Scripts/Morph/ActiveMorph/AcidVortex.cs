using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortex : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private AcidVortexAOE acidVortexAOEPrefab;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnAcidVortex();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnAcidVortex();
        }
    }


    private void SpawnAcidVortex()
    {
        var acidVortexAOE = ObjectPooler.Instance.GetOrCreatePooledObject(acidVortexAOEPrefab.gameObject).GetComponent<AcidVortexAOE>();
        acidVortexAOE.Initialize(damageHandler);
        
        var AOETransform = acidVortexAOE.transform;
        AOETransform.SetParent(transform);
        AOETransform.localPosition = Vector3.zero;

        // acidVortex.GetComponent<AOE_DELAY>().SetDamageDealer(GetComponent<DamageHandler>());
        // acidVortex.GetComponent<AOE_DELAY>().StartCoroutine("TriggerActivation");
        // acidVortex.GetComponent<AOE_UPDATE>().SetDamageDealer(GetComponent<DamageHandler>());
        // acidVortex.GetComponent<AOE_UPDATE>().StartCoroutine("TriggerActivation");
        // acidVortex.transform.parent = transform;
        //acidVortex.transform.position = transform.position;

    }
}
