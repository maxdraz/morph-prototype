using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCry : ActiveMorph
{
    static int meleeDamagePrerequisit = 25;
    static int intimidationPrerequisit = 200;


    [SerializeField] private GameObject battleCryAOE;

    public bool nextHitCrit;

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
