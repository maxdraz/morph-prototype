using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private WeaponMorphAttackData[] lightAttacks;
    private WeaponMorphAttackData[] heavyAttacks;

    private Queue<Attack> attackQueue; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TryQueueAttack()
    {
        
    }

    public void Initialize(WeaponMorphAttackData[] lAttacks, WeaponMorphAttackData[] hAttacks)
    {
        lightAttacks = lAttacks;
        heavyAttacks = hAttacks;
    }
}
