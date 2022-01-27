using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrifyingHowl : ActiveMorph
{
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private float range;
    DamageHandler damageHandler;


    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }
    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            if (GetComponent<Stealth>().stealthMode) 
            {
                Howl();
                return true;
            }
            return false;
        }
        return false;
    }

    private void Howl()
    {


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                //if (they had not detected you) 
                {
                    float distance = (transform.position - hitCollider.transform.position).magnitude;
                    float fortitudeDamageMultiplier = range - distance;
                    hitCollider.GetComponent<DamageHandler>().ApplyDamage(new FortitudeDamageData(fortitudeDamage * (1 + fortitudeDamageMultiplier)), damageHandler);
                }
            }
        }
    }

    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
    }
}
