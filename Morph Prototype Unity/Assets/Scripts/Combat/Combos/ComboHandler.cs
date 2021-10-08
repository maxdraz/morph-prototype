using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboHandler
{
    public AttackSet attackSet;
    public bool isExectuing;

    public ComboHandler(AttackSet attackSet)
    {
        this.attackSet = attackSet;
        
        isExectuing = false;
    }

    public void TryQueueAttack(bool isLightAttack)
    {
        
    }

}
