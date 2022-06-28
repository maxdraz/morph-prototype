using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCry : ActiveMorph
{
    static int meleeDamagePrerequisit = 25;
    static int intimidationPrerequisit = 200;

    [SerializeField] private GameObject battleCryAOE;

    public bool nextHitCrit;

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
        SpendEnergy(energyCost);
        SpendStamina(staminaCost);

        GameObject battleCry = ObjectPooler.Instance.GetOrCreatePooledObject(battleCryAOE);
        battleCry.transform.position = transform.position;
        battleCry.transform.parent = transform;

        //Next melee attack is a guaranateed critical hit for duration
        
        StartCoroutine("BattleCryDuration");
    }

    IEnumerator BattleCryDuration()
    {
        Debug.Log("BattleCry duration started");
        nextHitCrit = true;
        yield return new WaitForSeconds(3.6f);
        nextHitCrit = false;
        Debug.Log("BattleCry duration is over");
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnBattleCry();
        }
    }
}
