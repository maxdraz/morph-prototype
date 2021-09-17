using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class HealAbility : Ability
{

    public HealAbility(Timer cooldown) : base(cooldown)
    {
        
    }
    
    public override void Use()
    {
        Debug.Log("healed");
        cooldown.Start();
    }
}
