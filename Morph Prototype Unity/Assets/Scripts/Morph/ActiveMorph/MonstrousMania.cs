using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrousMania : ActiveMorph
{



    [SerializeField] private float attackSpeedBoost;
    [SerializeField] private float attackSpeedBoostDuration;

    [SerializeField] private float lifeStealBoost;
    [SerializeField] private float lifeStealBoostDuration;

    bool attackSpeedOnCooldown;

    //static Prerequisite[] StatPrerequisits;

    private void Start()
    {
        //WriteToPrerequisiteArray();
    }

    //void WriteToPrerequisiteArray()
    //{
    //    statPrerequisits = new Prerequisite[StatPrerequisits.Length];
    //
    //    for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
    //    {
    //        statPrerequisits[i] = StatPrerequisits[i];
    //        Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
    //    }
    //}

    private void Update()
    {
        if (cooldown.JustCompleted) 
        {
            attackSpeedOnCooldown = false;
        }

        if (Input.GetKeyDown(testInput))
        {
            if (!attackSpeedOnCooldown)
            {
                AttackSpeedBoost();
            }
            else
            {  
                LifeStealBoost(); 
            }
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
        attackSpeedOnCooldown = true;
        yield return new WaitForSeconds(attackSpeedBoostDuration);
        
        yield return null;
    }

    IEnumerator LifeStealBoost()
    {
        
        yield return new WaitForSeconds(lifeStealBoostDuration);

        yield return null;
    }
}
