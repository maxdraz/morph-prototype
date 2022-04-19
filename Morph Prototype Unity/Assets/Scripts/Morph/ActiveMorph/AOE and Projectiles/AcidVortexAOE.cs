using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortexAOE : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onPullEffects;
    [SerializeField] private List<OnHitEffectDataContainer> onPushEffects;
    private DamageHandler damageDealer;

    private void OnEnable()
    {
        StartCoroutine(ExecuteAOECoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Initialize(DamageHandler dmgDealer)
    {
        damageDealer = dmgDealer;
    }

    private IEnumerator ExecuteAOECoroutine()
    {
        yield return new WaitForSeconds(3);
        // suck in 
       // yield return new WaitForSeconds()
    }

    private void OnValidate()
    {
        foreach (var onHitEffectDataContainer in onPullEffects)
        {
            onHitEffectDataContainer.OnValidate();
        }

        foreach (var onHitEffectDataContainer in onPushEffects)
        {
            onHitEffectDataContainer.OnValidate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var dmgTaker = other.GetComponentInChildren<DamageHandler>();
        if(!dmgTaker || dmgTaker.gameObject == gameObject) return;
        
        OnHitEffectDataContainer.ApplyOnHitEffects(ref onPullEffects, dmgTaker, damageDealer);
    }
}
