using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;
    [SerializeField] private ConeProjectileSpawner poxShotSpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {

        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),
    };

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
            SpendEnergy(energyCost);
            SpendStamina(staminaCost);
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
