using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortex : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private GameObject acidVortexAOE;



    public Prerequisite[] StatPrerequisits;

    private void Start()
    {
        WriteToPrerequisiteArray();
    }

    void WriteToPrerequisiteArray()
    {
        statPrerequisits = new Prerequisite[StatPrerequisits.Length];

        for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
        {
            statPrerequisits[i] = StatPrerequisits[i];
            Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
        }
    }

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
        GameObject acidVortex = ObjectPooler.Instance.GetOrCreatePooledObject(acidVortexAOE);
        acidVortex.GetComponent<AOE_DELAY>().SetDamageDealer(GetComponent<DamageHandler>());
        acidVortex.GetComponent<AOE_DELAY>().StartCoroutine("TriggerActivation");
        acidVortex.GetComponent<AOE_UPDATE>().SetDamageDealer(GetComponent<DamageHandler>());
        acidVortex.GetComponent<AOE_UPDATE>().StartCoroutine("TriggerActivation");
        acidVortex.transform.parent = transform;
        acidVortex.transform.position = transform.position;

    }
}
