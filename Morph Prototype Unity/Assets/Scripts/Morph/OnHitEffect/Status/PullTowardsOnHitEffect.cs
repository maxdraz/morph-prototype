using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PullTowardsData : OnHitEffectData, IPullTowards
{
    [SerializeField] private float pullForce;

    public PullTowardsData (float pullForce = 100)
    {
        this.pullForce = pullForce;
    }

    public float PullForce
    {
        get => pullForce;
        set => pullForce = value;
    }

    public override object Clone()
    {
        return new PullTowardsData(pullForce);
    }
}

[CreateAssetMenu(fileName = "Pull Towards", menuName = "On Hit Effects/Pull Towards")]
public class PullTowardsOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PullTowardsData(100);
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is PullTowardsData pullTowardsData)
        {
            damageTaker.ApplyDamage(pullTowardsData, damageDealer);
        }
    }
}
