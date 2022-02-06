using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    static int fortitudePrerequisit;

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

    private void AdrenalineBoost()
    {
        GetComponent<Stamina>().AddStamina(adrenalineBoost);
    }
}
