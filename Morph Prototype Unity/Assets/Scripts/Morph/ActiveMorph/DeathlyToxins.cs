using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlyToxins : ActiveMorph
{
    [SerializeField] private DeathlyToxinsPrerequisiteData prerequisiteData;


    [SerializeField] private RadialProjectileSpawner deathlyToxinsSpawner;

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
            SpawnDeathlyToxins();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnDeathlyToxins();
        }
    }

    private void SpawnDeathlyToxins()
    {
        var projectiles = deathlyToxinsSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }



    private void OnValidate()
    {
        deathlyToxinsSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        deathlyToxinsSpawner?.OnDrawGizmos(transform);
    }
}
