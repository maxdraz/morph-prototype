using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAttack : Attack
{
   [SerializeField] private bool requiresUnlock;
   [SerializeField] private float duration = 1;
   [SerializeField] private float inputWindowBeforeAttackEnd = 0.2f;
   [SerializeField] private float inputWindowAfterAttackEnd = 0.2f;

   public WeaponAttack(bool requiresUnlock = false, float duration = 1, float inputWindowBeforeAttackEnd = 0.2f, float inputWindowAfterAttackEnd = 0.2f)
   {
      this.requiresUnlock = requiresUnlock;
      this.duration = duration;
      this.inputWindowBeforeAttackEnd = inputWindowBeforeAttackEnd;
      this.inputWindowAfterAttackEnd = inputWindowAfterAttackEnd;
   }
   
   
   public virtual object Clone()
   {
      return new WeaponAttack(requiresUnlock, duration, inputWindowAfterAttackEnd, inputWindowBeforeAttackEnd);
   }
}
