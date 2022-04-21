using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningSpray : ActiveMorph
{


    [SerializeField] private RadialProjectileSpawner weakeningSpraySpawner;

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
            SpawnWeakeningSpray();
            return true;
        }
        return false;
    }

    private void Update()
    {
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
