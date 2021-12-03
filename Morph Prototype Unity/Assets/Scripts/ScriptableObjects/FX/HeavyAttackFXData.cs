using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heavy Attack FX", menuName = "FX Data/Heavy Attack FX")]
public class HeavyAttackFXData : WeaponAttackFXData
{
   [Header("Heavy attack specific")]
   public GameObject OnChargeParticles;
}
