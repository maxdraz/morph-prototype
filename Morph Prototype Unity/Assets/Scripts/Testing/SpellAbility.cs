using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class SpellAbility : Ability
{
    public SpellAbility(Timer cooldownTimer) : base(cooldownTimer)
    {
        
    }
    
    public override void Use()
    {
       if(!IsReady()) return;
       Debug.Log("spell cast");
        cooldown.Start();
    }
}
