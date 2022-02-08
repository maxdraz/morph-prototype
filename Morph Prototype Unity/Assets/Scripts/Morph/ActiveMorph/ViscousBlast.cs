using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViscousBlast : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;


    [SerializeField] private RadialProjectileSpawner viscousBlastSpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
{
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
};

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnViscousblast();
            return true;
        }
        return false;
    }

    private void SpawnViscousblast()
    {
        var projectiles = viscousBlastSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }



    private void OnValidate()
    {
        viscousBlastSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        viscousBlastSpawner?.OnDrawGizmos(transform);
    }
}