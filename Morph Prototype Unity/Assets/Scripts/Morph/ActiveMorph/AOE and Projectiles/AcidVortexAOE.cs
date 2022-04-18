using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVortexAOE : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onPull;
    [SerializeField] private List<OnHitEffectDataContainer> onPush;
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
        yield return null;
    }

    private void OnValidate()
    {
        foreach (var onHitEffectDataContainer in onPull)
        {
            onHitEffectDataContainer.OnValidate();
        }

        foreach (var onHitEffectDataContainer in onPush)
        {
            onHitEffectDataContainer.OnValidate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("TRIGGERRERE ENTERERERD");
        var dmgTaker = other.GetComponentInChildren<DamageHandler>();
        if(!dmgTaker || dmgTaker.gameObject == gameObject) return;
        print("APPLYING ON HIT EFFECTSSSSSSS");
        OnHitEffectDataContainer.ApplyOnHitEffects(ref onPull, dmgTaker, damageDealer);
    }
}
