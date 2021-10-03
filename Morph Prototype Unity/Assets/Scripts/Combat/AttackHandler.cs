using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private WeaponMorphAttackData[] lightAttackData;
    private WeaponMorphAttackData[] heavyAttackData;

    private Queue<Attack> attackQueue;
    private Attack currentAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackQueue.Count < 1) return; //no queued attacks

        if (!attackQueue.Peek().completed)
        {
            currentAttack = attackQueue.Dequeue();
        }
    }

    void TryQueueLightAttack()
    {
        if (lightAttackData.Length < 1) return;
        
        // if attack
        
    }
    
    void TryQueueHeavyAttack()
    {
       
    }

    public void InitializeAttackData(WeaponMorphAttackData[] lAttacks, WeaponMorphAttackData[] hAttacks)
    {
        lightAttackData = lAttacks;
        heavyAttackData = hAttacks;
    }
}
