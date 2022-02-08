using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonNeedleBarrage : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;

    [SerializeField] private RadialProjectileSpawner poisonNeedleBarrageSpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),
    };

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnPoisonNeedleBarrage();
            return true;
        }
        return false;
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
