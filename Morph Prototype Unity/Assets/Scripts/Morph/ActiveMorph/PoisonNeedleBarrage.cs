using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonNeedleBarrage : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;
    static int rangedDamagePrerequisit = 25;

    [SerializeField] private RadialProjectileSpawner poisonNeedleBarrageSpawner;

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
            SpawnPoisonNeedleBarrage();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnPoisonNeedleBarrage();
        }
    }

    private void SpawnPoisonNeedleBarrage()
    {
        var projectiles = poisonNeedleBarrageSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    

    private void OnValidate()
    {
        poisonNeedleBarrageSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poisonNeedleBarrageSpawner?.OnDrawGizmos(transform);
    }
}
