using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCry : ActiveMorph
{
    static int meleeDamagePrerequisit = 25;
    static int intimidationPrerequisit = 200;


    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("meleeDamage", meleeDamagePrerequisit),
        new Prerequisite("intimidation", intimidationPrerequisit)
    };

    [SerializeField] private RadialProjectileSpawner battleCrySpawner;

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
        var projectiles = battleCrySpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
                projectile.GetComponent<BattleCryAOE>().sourceCreature = this.gameObject;
            }
    }



    private void OnValidate()
    {
        battleCrySpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        battleCrySpawner?.OnDrawGizmos(transform);
    }
}
