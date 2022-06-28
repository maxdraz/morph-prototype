using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    static int fortitudePrerequisite = 35;
    [SerializeField] private GameObject adrenalineRushParticles;
    [SerializeField] private float adrenalineBoost;
    private Stamina stamina;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();

        stamina = GetComponent<Stamina>();
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            AdrenalineBoost();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            AdrenalineBoost();
        }
    }

    private void AdrenalineBoost()
    {
        GameObject boost = ObjectPooler.Instance.GetOrCreatePooledObject(adrenalineRushParticles);
        boost.transform.position = transform.position;
        boost.transform.parent = transform;
        stamina.AddStamina(adrenalineBoost);
    }
}
