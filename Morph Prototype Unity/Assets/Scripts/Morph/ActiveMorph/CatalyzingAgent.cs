using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalyzingAgent : ActiveMorph
{
    [SerializeField] private CatalyzingAgentPrerequisiteData prerequisiteData;


    [SerializeField] private ConeProjectileSpawner catalyzingAgentSpawner;
    [SerializeField]
    private float chemicalDamageStatMultiplier = .5f;
    float damageToDeal;

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
