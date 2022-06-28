using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrifyingHowl : ActiveMorph
{
    [SerializeField] private GameObject terrifyingHowlParticles;
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private float range;
    private DamageHandler damageHandler;

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

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            Howl();
        }
    }

    private void Howl()
    {
        GameObject howl = ObjectPooler.Instance.GetOrCreatePooledObject(terrifyingHowlParticles);
        howl.transform.position = transform.position;
        howl.transform.parent = transform;

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
}
