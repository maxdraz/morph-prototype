using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningSpray : ActiveMorph
{
    [SerializeField] private RadialProjectileSpawner weakeningSpraySpawner;
    
    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnWeakeningSpray();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnWeakeningSpray();
        }
    }

    private void SpawnWeakeningSpray()
    {
        var projectiles = weakeningSpraySpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<AOE>().SetDamageDealer(GetComponent<DamageHandler>());
                //projectile.transform.rotation = transform.rotation;
                projectile.transform.parent = transform;
            }
    }

    private void OnValidate()
    {
        weakeningSpraySpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        weakeningSpraySpawner?.OnDrawGizmos(transform);
    }
}
