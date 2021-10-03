using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponMorph : MonoBehaviour
{
    protected AttackHandler attackHandler;
    protected List<LightAttack> lightAttacks;
    protected List<HeavyAttack> heavyAttacks;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        var existingWeaponMorphs = GetComponents(typeof(WeaponMorph)).ToList();
        if (existingWeaponMorphs.Count > 1)
        {
            Destroy(existingWeaponMorphs[0]);
        }
        
        attackHandler = GetComponent<AttackHandler>();
    }

    private void Start()
    {
        attackHandler.SetAttackData(lightAttacks, heavyAttacks);
    }
}
