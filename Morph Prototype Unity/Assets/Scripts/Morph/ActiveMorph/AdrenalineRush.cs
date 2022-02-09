using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    static int fortitudePrerequisit = 35;
    [SerializeField] private GameObject adrenalineRushParticles;
    [SerializeField] private float adrenalineBoost;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("fortitude", fortitudePrerequisit),
    };

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            AdrenalineBoost();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            AdrenalineBoost();
        }
    }

    private void AdrenalineBoost()
    {
        ObjectPooler.Instance.GetOrCreatePooledObject(adrenalineRushParticles);
        GetComponent<Stamina>().AddStamina(adrenalineBoost);
    }
}
