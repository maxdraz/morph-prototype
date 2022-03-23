using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NormalDamageBasedOnEnemyHealthData : OnHitEffectData, IPhysicalDamage
{
    private float morphDamage;
    [SerializeField] private bool applyIfBelowPercentage;
    [SerializeField] [Range(0,1)] private float enemyHealthPercentage;
    [SerializeField] private float strikeModifier;
    [SerializeField] private float weaponCritChance;
    private float physicalDamageDealt;

    public NormalDamageBasedOnEnemyHealthData(
        float morphDamage = 0, float strikeModifier = 1, float physicalDamageDealt = 0, bool applyIfBelowPercentage = false, 
        float enemyHealthPercentage = 0)
    {
        this.morphDamage = morphDamage;
        this.applyIfBelowPercentage = applyIfBelowPercentage;
        this.enemyHealthPercentage = enemyHealthPercentage;
        this.strikeModifier = strikeModifier;
        this.physicalDamageDealt = physicalDamageDealt;
    }

    public override object Clone()
    {
        return new NormalDamageBasedOnEnemyHealthData(morphDamage, strikeModifier, physicalDamageDealt, applyIfBelowPercentage,
            enemyHealthPercentage);
    }

    public float MorphDamage { 
        get => morphDamage;
        set => morphDamage = value;
    }
    public float StrikeModifier
    {
        get => strikeModifier;
        set => strikeModifier = value;
    }
    public float PhysicalDamageDealt
    {
        get => physicalDamageDealt;
        set => physicalDamageDealt = value;
    }
    public float WeaponCritChance
    {
        get => weaponCritChance;
        set => weaponCritChance = value;
    }

    public bool ConditionsAreMet(Health damageTakerHealth)
    {
        return (applyIfBelowPercentage && damageTakerHealth.CurrentHealthAsPercentage <= enemyHealthPercentage)
               || (!applyIfBelowPercentage && damageTakerHealth.CurrentHealthAsPercentage >= enemyHealthPercentage);
    }
    
}


[CreateAssetMenu(fileName = "Normal Physical Damage Based On Enemy Health", menuName = "On Hit Effects/Normal Physical Damage Based On Enemy Health")]
public class NormalPhysicalDamageBasedOnEnemyHealthOnHitEffect : NormalPhysicalDamageOnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new NormalDamageBasedOnEnemyHealthData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is NormalDamageBasedOnEnemyHealthData damageBasedOnEnemyHealthData)
        {
            if(damageBasedOnEnemyHealthData.ConditionsAreMet(damageTaker.Health))
            {
                base.ApplyOnHitEffect(data, damageTaker, damageDealer);
            }
        }
    }
}
