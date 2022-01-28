using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineRush : ActiveMorph
{
    [SerializeField] private float adrenalineBoost;



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
