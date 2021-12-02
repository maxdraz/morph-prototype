using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class OutdatedOnHitEffect
{
   public OutdatedOnHitEffect() {
   }

   public abstract void Reset();

   public abstract void Apply(OutdatedDamageHandler outdatedDamageTaker);
}
