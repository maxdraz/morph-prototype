using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCry : ActiveMorph
{
    static int meleeDamagePrerequisit = 25;
    static int intimidationPrerequisit = 200;

    [SerializeField] private GameObject battleCryAOE;
    float duration;

    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("meleeDamage", meleeDamagePrerequisit),
        new Prerequisite("intimidation", intimidationPrerequisit)
    };


    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnBattleCry();
            return true;
        }
        return false;
    }

    private void SpawnBattleCry()
    {
        ObjectPooler.Instance.GetOrCreatePooledObject(battleCryAOE);    
        //Next melee attack is a guaranateed critical hit for duration
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnBattleCry();
        }
    }

    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
    }
}
