using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalyzingAgent : ActiveMorph
{
    static int chemicalDamagePrerequisite = 25;


    [SerializeField] private ConeProjectileSpawner catalyzingAgentSpawner;
    [SerializeField]
    private float chemicalDamageStatMultiplier = .5f;
    float damageToDeal;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
    };

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnCatalyzingAgent();
            return true;
        }
        return false;
    }

    private void SpawnCatalyzingAgent()
    {
        var projectiles = catalyzingAgentSpawner?.Spawn(transform);
        damageToDeal = GetComponent<Stats>().totalChemicalDamage * chemicalDamageStatMultiplier;

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
                projectile.GetComponent<CatalyzingAgentProjectile>().damage = damageToDeal;
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnCatalyzingAgent();
        }
    }

    private void OnValidate()
    {
        catalyzingAgentSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        catalyzingAgentSpawner?.OnDrawGizmos(transform);
    }
}
