using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Morph/WeaponMorph/WeaponMorphData")]
public class WeaponMorphData : MorphData
{
    
    public CreatureType[] creatures;
    public WeaponMorphAttackData[] basicAttackData;
    public WeaponMorphAttackData[] heavyAttackData;

}
