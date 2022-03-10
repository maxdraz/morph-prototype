using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_DELAY : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    public DamageHandler damageDealer;

    public Collider collider;
    public float delayPeriod;
    [SerializeField] private float duration;


    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        damageDealer = dmgDealer;
    }
    void Awake()
    {
        collider = GetComponent<Collider>();
        delayPeriod = GetComponent<ParticleSystem>().main.startDelay.constant;
        
    }

    public IEnumerator TriggerActivation()
    {
        yield return new WaitForSeconds(delayPeriod);
        collider.enabled = !collider.enabled;
        Debug.Log(GetType().Name + "TriggerActivation");
        yield return new WaitForSeconds(duration);

        collider.enabled = !collider.enabled;
        Debug.Log(GetType().Name + "TriggerDeactivation");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // dont collide with self
        if (other.gameObject == damageDealer.gameObject) return;
        // deal damage to enemy
        var otherDamageHandler = other.gameObject.GetComponentInChildren<DamageHandler>();

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
