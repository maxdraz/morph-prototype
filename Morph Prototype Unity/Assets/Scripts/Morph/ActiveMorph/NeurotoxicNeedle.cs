using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeurotoxicNeedle : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;

    [SerializeField] private RadialProjectileSpawner neurotoxicNeedleSpawner;

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
            SpawnNeurotoxicNeedle();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnNeurotoxicNeedle();
        }
    }

    private void SpawnNeurotoxicNeedle()
    {
        var projectiles = neurotoxicNeedleSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    private void OnValidate()
    {
        neurotoxicNeedleSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        neurotoxicNeedleSpawner?.OnDrawGizmos(transform);
    }
}
