using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_DelayAttack : HeavyWeaponAttack
{
    private Timer cooldown;
    
    public T_DelayAttack(GameObject owner, WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects) 
        : base(owner, weaponAttackData, baseOnHitEffects)
    {
        cooldown = new Timer(2);
    }

    public override void OnStart()
    {
        base.OnStart();
        
        // set player speed 0 
        // owenr 
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        cooldown.CountDown(Time.deltaTime);

        if (cooldown.JustFinished)
        {
            
            // set speed back 
            // spawn particle
        }
    }
    
    
    // void OnAttackHit ( Attack attack)
    // if attack is WeaponAttack
        // if weapoponAttack.WasCrit 
            // crit count ++
            
            
    // void OnAttackWillHit ( attack )
        // if attack acid add attack.onhiteffect.add ( acid)
    // void OnAttackFinished();
}
