using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - make all member vars private
//TODO - implement remaining hitbox types

public enum WeaponMorphType
{
    Limb,
    Head,
    Tail
}

public enum WeaponAttackType
{
    Light,
    Heavy
}

public class WeaponMorphAttackHandler : MonoBehaviour
{
    private MorphLoadout loadout;
    
    private LimbWeaponMorph limbWeaponMorph;
    private HeadWeaponMorph headWeaponMorph;
    private TailWeaponMorph tailWeaponMorph;

    private BoxHitbox boxHitbox;
    private SphericalHitbox sphericalHitbox;

    private WeaponAttack currentWeaponAttack;
    private List<WeaponAttack> attackQueue;
    private Timer attackTimer;
    private Timer inputWindowTimer;

    // Start is called before the first frame update
    void Awake()
    {
        loadout = GetComponent<MorphLoadout>();
        attackQueue = new List<WeaponAttack>();

        boxHitbox = GetComponentInChildren<BoxHitbox>();
        sphericalHitbox = GetComponentInChildren<SphericalHitbox>();
    }

    private void OnEnable()
    {
        if (loadout) loadout.MorphLoadoutChanged += OnMorphLoadoutChanged;
        if (boxHitbox) boxHitbox.Hit += OnAttackHit;
        if (sphericalHitbox) sphericalHitbox.Hit += OnAttackHit;
        
    }

    private void OnDisable()
    {
        if (loadout) loadout.MorphLoadoutChanged -= OnMorphLoadoutChanged;
        if (boxHitbox) boxHitbox.Hit -= OnAttackHit;
        if (sphericalHitbox) sphericalHitbox.Hit -= OnAttackHit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LimbLightAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LimbHeavyAttack();
        }

        InputWindowAfterAttack();
        if(attackQueue.Count < 1) return;
        ExecuteAttacks();
    }

    private void InputWindowAfterAttack()
    {
        if(attackQueue.Count > 0) return;
        if (!inputWindowTimer.IsFinished())
        {
            inputWindowTimer.CountDown(Time.deltaTime);
            print("input next window");
        }
    }

    private void ExecuteAttacks()
    {
        currentWeaponAttack = attackQueue[0];

        RestartTimerIfNecessary2();
        
        if (attackTimer.JustStarted)
            StartAttack();

        if (attackTimer.CountDown(Time.deltaTime))
            UpdateAttack();
        
        //if()

        if (attackTimer.JustFinished)
            FinishAttack();
    }

    private void RestartTimerIfNecessary()
    {
        if (attackTimer.Duration != currentWeaponAttack.Duration)
        {
            attackTimer.Restart(currentWeaponAttack.Duration);
        }
    }
    
    private void RestartTimerIfNecessary2()
    {
        if (attackTimer == null || attackTimer.IsFinished())
        {
            attackTimer = new Timer(currentWeaponAttack.Duration);
        } 
    }

    private Hitbox GetAppropriateHitbox(HitboxType hitboxType)
    {
        switch (hitboxType)
        {
            case HitboxType.Box:
                    return boxHitbox;
                break;
            case HitboxType.Spherical:
                    return sphericalHitbox;
                break;
            case HitboxType.Cone:
                break;
            case HitboxType.None:
                break;
        }

        return null;
    }

    private void StartAttack()
    {
        print("attack started");
        var hitbox = GetAppropriateHitbox(currentWeaponAttack.Data.HitBoxType);
        if(hitbox) hitbox.Activate();
        
        currentWeaponAttack.OnStart();
    }
    
    private void OnAttackHit(DamageHandler damageHandler, Collider other)
    {
        currentWeaponAttack.OnHit(damageHandler, other);
    }

    private void UpdateAttack()
    {
        print("attack updating");
        currentWeaponAttack.OnUpdate();
    }

    private void FinishAttack()
    {
        print("attack finished");
        // call finish on attack
        currentWeaponAttack.OnFinish();
        
        //turn off hitbox
        var hitbox = GetAppropriateHitbox(currentWeaponAttack.Data.HitBoxType);
        if(hitbox) hitbox.Deactivate();
        
        //start input window and remove this attack from queue
        inputWindowTimer = new Timer(currentWeaponAttack.InputWindowAfterAttack);
        attackQueue.RemoveAt(0);
        print("queue count: " + attackQueue.Count);
    }

    private void TryQueueAttack(WeaponMorphType morphType, WeaponAttackType attackType)
    {
        switch (morphType)
        {
           case WeaponMorphType.Limb:
               TryQueueAttack(limbWeaponMorph, attackType);
               break;
           case WeaponMorphType.Head:
               TryQueueAttack(headWeaponMorph, attackType);
               break;
           case WeaponMorphType.Tail:
               TryQueueAttack(tailWeaponMorph, attackType);
               break;
        }
    }

    private void TryQueueAttack(WeaponMorph weaponMorph, WeaponAttackType attackType)
    {
        if(weaponMorph == null) return;
        if (!CanQueueNextAttack(weaponMorph, attackType))
        {
            print("can queue: " +CanQueueNextAttack(weaponMorph, attackType));
            return;
        }

        var currentAttack = weaponMorph.GetCurrentAttack(attackType);
        if (currentAttack != null)
        {
            print("queue successful");
            attackQueue.Add(currentAttack);
            weaponMorph.AdvanceCombo(attackType);
            print(attackQueue.Count);
        }
    }

    private bool CanQueueNextAttack(WeaponMorph weaponMorph, WeaponAttackType attackType)
    {
        if (currentWeaponAttack == null) return true;
        if (attackQueue.Count > 1) return false;
        
        if (attackTimer.CurrentTime <= currentWeaponAttack.InputWindowDuringAttack
            || !inputWindowTimer.IsFinished()) // within input window
            return true;

        return false;
    }

    public void LimbLightAttack()
    {
        TryQueueAttack(WeaponMorphType.Limb, WeaponAttackType.Light);
    }

    public void LimbHeavyAttack()
    {
        TryQueueAttack(WeaponMorphType.Limb, WeaponAttackType.Heavy);

    }

    private void OnMorphLoadoutChanged(WeaponMorph morph)
    {
        if (morph is LimbWeaponMorph limb)
        {
            limbWeaponMorph = limb;
        } else if (morph is HeadWeaponMorph head)
        {
            headWeaponMorph = head;
        }else if (morph is TailWeaponMorph tail)
        {
            tailWeaponMorph = tail;
        }
    }

   
}
