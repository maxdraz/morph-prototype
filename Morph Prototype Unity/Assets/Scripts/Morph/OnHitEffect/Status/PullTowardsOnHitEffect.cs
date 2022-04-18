using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PullTowardsData : OnHitEffectData
{
    [SerializeField] private PullTowardsDebuff pullTowardsDebuff;
    
    // [SerializeField] private float pullForce;
    //[SerializeField] private Transform pullTowardsThis;
    // [SerializeField] private ForceMode forceMode;
    //
    public PullTowardsDebuff PullTowardsDebuff => pullTowardsDebuff;
    //
    // public Transform PullTowardsThis { 
    //     get => pullTowardsThis;
    //     set => pullTowardsThis = value;
    // }
    // public float PullForce
    // {
    //     get => pullForce;
    //     set => pullForce = value;
    // }
    // public ForceMode ForceMode
    // {
    //     get => forceMode; 
    //     set => forceMode = value;
    // }

    // public PullTowardsData (float pullForce = 100, Transform pullTowardsThis = null, ForceMode forceMode = ForceMode.Acceleration)
    // {
    //     // this.pullForce = pullForce;
    //     // this.pullTowardsThis = pullTowardsThis;
    //     // this.forceMode = forceMode;
    //     
    // }

    public PullTowardsData()
    {
        pullTowardsDebuff = new PullTowardsDebuff();
    }

    public override object Clone()
    {
        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Pull Towards", menuName = "On Hit Effects/Pull Towards")]
public class PullTowardsOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PullTowardsData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is PullTowardsData pullTowardsData)
        {
           // damageTaker.ApplyDamage(pullTowardsData, damageDealer);
           var debuff = pullTowardsData.PullTowardsDebuff;
           var debuffClone = new PullTowardsDebuff(debuff.PullForce, debuff.ForceMode, debuff.Target, debuff.Duration,
               damageTaker, damageDealer);
           
           damageTaker.AddPhysicsDebuff(debuffClone);
        }
    }
}
