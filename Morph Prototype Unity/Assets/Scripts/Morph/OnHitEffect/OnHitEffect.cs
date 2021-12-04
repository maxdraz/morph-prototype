using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitEffect : ScriptableObject
{
   public abstract OnHitEffectData GetData();

   public abstract void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer);
}
