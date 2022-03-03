using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;

    private DamageHandler damageDealer;
    [SerializeField] private float duration;


    private void Start()
    {
        if (duration > 0) 
        {
            StartCoroutine("Duration");
        }
    }

    IEnumerator Duration() 
    {
        yield return new WaitForSeconds(duration);

        GetComponent<Collider>().enabled = false;

        yield return null;
    }

    private void OnValidate()
    {
        OnHitEffectDataContainer.OnValidate(ref onHitEffects);
    }

    public void SetDamageDealer(DamageHandler dmgDealer)
    {
        this.damageDealer = dmgDealer;
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
