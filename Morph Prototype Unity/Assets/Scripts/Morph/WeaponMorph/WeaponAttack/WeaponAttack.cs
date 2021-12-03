using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAttack : Attack
{
   [SerializeField] protected bool requiresUnlock;
   [SerializeField] protected HitboxType hitboxType;
   [SerializeField] protected float duration;
   [SerializeField] protected float inputWindowBeforeAttackEnd;
   [SerializeField] protected float inputWindowAfterAttackEnd;
   private WeaponMorph owner;

   public float Duration => duration;
   public float InputWindowBeforeAttackEnd => inputWindowBeforeAttackEnd;
   public float InputWindowAfterAttackEnd => inputWindowAfterAttackEnd;
   public WeaponMorph Owner
   {
      get => owner;
      set => owner = value;
   }
   public HitboxType HitboxType => hitboxType;
   
   
   public WeaponAttack(bool requiresUnlock = false, float duration = 1, float inputWindowBeforeAttackEnd = 0.2f, 
      float inputWindowAfterAttackEnd = 0.2f, HitboxType hitboxType = HitboxType.Box, List<OnHitEffectDataContainer> onHitEffects = null)
   {
      this.requiresUnlock = requiresUnlock;
      this.duration = duration;
      this.inputWindowBeforeAttackEnd = inputWindowBeforeAttackEnd;
      this.inputWindowAfterAttackEnd = inputWindowAfterAttackEnd;
      this.hitboxType = hitboxType;
      this.onHitEffects = onHitEffects;
   }

   public WeaponAttack(WeaponAttack otherAttack)
   {
      this.requiresUnlock = otherAttack.requiresUnlock;
      this.duration = otherAttack.duration;
      this.inputWindowBeforeAttackEnd = otherAttack.inputWindowBeforeAttackEnd;
      this.inputWindowAfterAttackEnd = otherAttack.inputWindowAfterAttackEnd;
      this.hitboxType = otherAttack.hitboxType;
      this.onHitEffects = otherAttack.onHitEffects;
   }


   public virtual void OnStart()
   {
   }

   public virtual void OnHit(DamageHandler damageTaker)
   {
      // on hit
      foreach (var onHitEffectDataContainer in onHitEffects)
      {
         onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data, damageTaker);
      }
   }

   public void OnUpdate()
   {
      // implement
   }

   public void OnFinish()
   {
      
   }
   
   public virtual object Clone()
   {
      if (base.Clone() is Attack attack)
         return new WeaponAttack(requiresUnlock, duration, inputWindowAfterAttackEnd, inputWindowBeforeAttackEnd,
            hitboxType, attack.OnHitEffects);
      
      return null;
   }
}
