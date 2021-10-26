using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackQueue
{
    public AttackSet attackSet;
    public bool isExectuing;

    public AttackQueue(AttackSet attackSet)
    {
        this.attackSet = attackSet;
        
        isExectuing = false;
    }

    public void TryQueueAttack(bool isLightAttack)
    {
        
    }

}
