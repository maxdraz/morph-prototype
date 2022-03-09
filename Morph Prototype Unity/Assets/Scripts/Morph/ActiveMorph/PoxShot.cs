using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;
    [SerializeField] private ConeProjectileSpawner poxShotSpawner;

    static Prerequisite[] StatPrerequisits = new Prerequisite[1]
    {

        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),
    };

    private void Start()
    {
        WriteToPrerequisiteArray();
    }

    void WriteToPrerequisiteArray()
    {
        statPrerequisits = new Prerequisite[StatPrerequisits.Length];

        for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
        {
            statPrerequisits[i] = StatPrerequisits[i];
            Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
        }
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnPoxShot();
            return true;
        }

        return false;
    }

    private void SpawnPoxShot()
    {
        SpendEnergy(energyCost);
        SpendStamina(staminaCost);
        var projectiles = poxShotSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnPoxShot();
        }
    }

    private void OnValidate()
    {
        poxShotSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poxShotSpawner?.OnDrawGizmos(transform);
    }
}
