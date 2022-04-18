using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_UPDATE : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    public DamageHandler damageDealer;

    [SerializeField] private float duration;
    [SerializeField] private float radius;
    [SerializeField] private float delayPeriod;
    public bool active;

    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        damageDealer = dmgDealer;
    }

    public IEnumerator TriggerActivation()
    {

        yield return new WaitForSeconds(delayPeriod);

        active = true;
        Debug.Log(GetType().Name + "TriggerActivation");
        yield return new WaitForSeconds(duration);

        active = false;
        Debug.Log(GetType().Name + "TriggerDeactivation");
        yield return null;
    }

    

    private void FixedUpdate()
    {
        // if (active) 
        // {
        //     AOEEffect();
        // }
    }

    void AOEEffect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name.Contains("Bear"))
            {
                print("BEAR DETECTED");
            }
            
            if (hitCollider.gameObject == damageDealer.gameObject) return;
            // deal damage to enemy
            var otherDamageHandler = hitCollider.gameObject.GetComponentInChildren<DamageHandler>();

            if (otherDamageHandler)
            {
                foreach (var onHitEffectDataContainer in onHitEffects)
                {
                    onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data, otherDamageHandler, damageDealer);
                    //print("should be applying damage");
                    // otherDamageHandler.ApplyDamage(onHitEffectDataContainer.Data, damageDealer);
                }
                return;
            }

        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if(!active) return;
    //     
    //     if (other.gameObject == damageDealer.gameObject) return;
    //     // deal damage to enemy
    //     var otherDamageHandler = other.gameObject.GetComponentInChildren<DamageHandler>();
    //
    //     if (otherDamageHandler)
    //     {
    //         foreach (var onHitEffectDataContainer in onHitEffects)
    //         {
    //             onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data, otherDamageHandler, damageDealer);
    //             //print("should be applying damage");
    //             // otherDamageHandler.ApplyDamage(onHitEffectDataContainer.Data, damageDealer);
    //         }
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     var dmgTaker = other.GetComponentInChildren<DamageHandler>();
    //     if(dmgTaker.gameObject == gameObject || !dmgTaker) return;
    //     
    //     foreach (var onHitEffectDataContainer in onHitEffects)
    //     {
    //         onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data,dmgTaker, damageDealer);
    //         //print("should be applying damage");
    //         // otherDamageHandler.ApplyDamage(onHitEffectDataContainer.Data, damageDealer);
    //     }
    // }
}
