using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrousMania : ActiveMorph
{
    static int meleeDamagePrerequisite = 30;
    static int agilityPrerequisite = 25;

    [SerializeField] private float attackSpeedBoost;
    [SerializeField] private float attackSpeedBoostDuration;

    [SerializeField] private float lifeStealBoost;
    [SerializeField] private float lifeStealBoostDuration;

    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("meleeDamage", meleeDamagePrerequisite),
        new Prerequisite("agility", agilityPrerequisite)
    };

    bool attackSpeedOnCooldown;

    private void Update()
    {
        if (cooldown.JustCompleted) 
        {
            attackSpeedOnCooldown = false;
        }
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            if (!attackSpeedOnCooldown)
            {
                AttackSpeedBoost();
                return true;
            }
            else
            {
                if (!cooldown.Completed) 
                {
                    LifeStealBoost();
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator AttackSpeedBoost()
    {

        yield return new WaitForSeconds(attackSpeedBoostDuration);
        
        yield return null;
    }

    IEnumerator LifeStealBoost()
    {

        yield return new WaitForSeconds(lifeStealBoostDuration);

        yield return null;
    }
}
