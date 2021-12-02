using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimbWeaponOutdatedMorph : WeaponOutdatedMorph
{
   public LimbWeaponOutdatedMorph(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data) 
      : base( owner,  ownerOutdatedDamageHandler,  data)
   {
      
   }
   
}
