using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimbWeaponMorph : WeaponMorph
{
   public LimbWeaponMorph(GameObject owner, DamageHandler ownerDamageHandler, WeaponMorphData data) 
      : base( owner,  ownerDamageHandler,  data)
   {
      
   }
   
}
