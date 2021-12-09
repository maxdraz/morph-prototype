using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO - remove if is player input check
//TODO - make all member vars private
//TODO - implement remaining hitbox types

[RequireComponent(typeof(MorphLoadout), typeof(DamageHandler))]
public class WeaponMorphAttackHandler : MonoBehaviour
{
    private MorphLoadout loadout;
    private CreatureVirtualController controller;
    private DamageHandler damageHandler;
    
    private LimbWeaponMorph limbWeaponMorph;
    private HeadWeaponMorph headWeaponMorph;
    private TailWeaponMorph tailWeaponMorph;

    private BoxHitbox boxHitbox;
    private SphericalHitbox sphericalHitbox;

    private WeaponAttack currentWeaponAttack;
    private WeaponMorph currentWeaponMorph;
    private List<WeaponAttack> attackQueue;

    private Timer attackTimer;
    private Timer inputWindowTimer;
    
    //public events
    public delegate void AttackQueuedHandler(ref WeaponAttack weaponAttack);
    public event AttackQueuedHandler AttackQueued;

    public delegate void AttackHasStartedHandler(ref WeaponAttack weaponAttack);
    public event AttackHasStartedHandler AttackHasStarted;

    // Start is called before the first frame update
    void Awake()
    {
        loadout = GetComponent<MorphLoadout>();
        controller = GetComponentInParent<CreatureVirtualController>();
        damageHandler = GetComponent<DamageHandler>();
        attackQueue = new List<WeaponAttack>();
        inputWindowTimer = new Timer(0);

        boxHitbox = GetComponentInChildren<BoxHitbox>();
        sphericalHitbox = GetComponentInChildren<SphericalHitbox>();
    }

    private void OnEnable()
    {
        if (loadout) loadout.MorphLoadoutChanged += OnMorphLoadoutChanged;
        if (boxHitbox) boxHitbox.Hit += OnAttackHit;
        if (sphericalHitbox) sphericalHitbox.Hit += OnAttackHit;

        if (controller)
        {
            controller.LimbLightAttack += LimbLightAttack;
            controller.LimbHeavyAttack += LimbHeavyAttack;
            controller.TailLightAttack += TailLightAttack;
            controller.TailHeavyAttack += TailHeavyAttack;
            controller.MouthLightAttack += MouthLightAttack;
            controller.MouthHeavyAttack += MouthHeavyAttack;
        }
        
    }

    private void OnDisable()
    {
        if (loadout) loadout.MorphLoadoutChanged -= OnMorphLoadoutChanged;
        if (boxHitbox) boxHitbox.Hit -= OnAttackHit;
        if (sphericalHitbox) sphericalHitbox.Hit -= OnAttackHit;
        
        if (controller)
        {
            controller.LimbLightAttack -= LimbLightAttack;
            controller.LimbHeavyAttack -= LimbHeavyAttack;
            controller.TailLightAttack -= TailLightAttack;
            controller.TailHeavyAttack -= TailHeavyAttack;
            controller.MouthLightAttack -= MouthLightAttack;
            controller.MouthHeavyAttack -= MouthHeavyAttack;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputWindowAfterAttack();
        if(attackQueue.Count < 1) return;
        ProcessAttackQueue();
    }

    private void UpdateInputWindowAfterAttack()
    {
        if (attackQueue.Count > 0)
        {
            return;
        }
        
        if (!inputWindowTimer.IsFinished())
        {
            inputWindowTimer.CountDown(Time.deltaTime);
            

            if (inputWindowTimer.JustFinished)
            {
                ResetCombo();
            }
        }
        else
        {
            // make creature stop looking in attack dir
            controller.GetComponent<Movement>().ClearLookRotationTransform();
        }
    }

    private void ResetCombo()
    {
        if(limbWeaponMorph)
            limbWeaponMorph.ResetCombo();
        
        if(tailWeaponMorph)
            tailWeaponMorph.ResetCombo();
        
        if(tailWeaponMorph)
            headWeaponMorph.ResetCombo();
    }

    private void ProcessAttackQueue()
    {
        //_currentOutdatedWeaponAttack = outdatedAttackQueue[0];
        currentWeaponAttack = attackQueue[0];
        

        RestartTimerIfNecessary(); // refactored
        
        if (attackTimer.JustStarted)
            StartAttack(); // refactored

        if (attackTimer.CountDown(Time.deltaTime))
            UpdateAttack(); // refactored
        
        if (attackTimer.JustFinished)
            FinishAttack();
    }

    private void RestartTimerIfNecessary()
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
        //var hitbox = GetAppropriateHitbox(_currentOutdatedWeaponAttack.Data.HitBoxType);
        var hitbox = GetAppropriateHitbox(currentWeaponAttack.HitboxType);
        if(hitbox) hitbox.Activate();
        
        currentWeaponAttack.OnStart();
        AttackHasStarted?.Invoke(ref currentWeaponAttack);
        
        // make crature face attack
        controller.GetComponent<Movement>().SetLookRotationTransform(Camera.main.transform);
    }
    
    private void OnAttackHit(DamageHandler damageTaker)
    {
        currentWeaponAttack.OnHit(damageTaker, damageHandler);
    }

    private void UpdateAttack()
    {
        currentWeaponAttack.OnUpdate();
    }

    private void FinishAttack()
    {
        // call finish on attack
        currentWeaponAttack.OnFinish();
        
        //turn off hitbox
        var hitbox = GetAppropriateHitbox(currentWeaponAttack.HitboxType);
        if(hitbox) hitbox.Deactivate();
        
        //start input window and remove this attack from queue
        inputWindowTimer = new Timer(currentWeaponAttack.InputWindowAfterAttackEnd);
        attackQueue.RemoveAt(0);
    }

    private void TryQueueAttack(WeaponMorph weaponMorphToQueue, bool isLightAttack =  true)
    {
        if (!weaponMorphToQueue || !CanQueueAttack()) return;

        var currentAttack = weaponMorphToQueue.GetCurrentAttack(isLightAttack);
        if (currentAttack != null)
        {
            if (currentAttack.Clone() is WeaponAttack currentAttackClone)
            {
                currentAttackClone.Owner = weaponMorphToQueue;
                attackQueue.Add(currentAttackClone);
                weaponMorphToQueue.AdvanceCombo(isLightAttack);
                AttackQueued?.Invoke(ref currentAttackClone);
            }
        }

    }

    private bool CanQueueAttack()
    {
        if (currentWeaponAttack == null) return true;
        if (attackQueue.Count > 1) return false;
        
        if (attackTimer.CurrentTime <= currentWeaponAttack.InputWindowBeforeAttackEnd
            || !inputWindowTimer.IsFinished()) // within input window
            return true;

        return false;
    }

    private void LimbLightAttack()
    {
        TryQueueAttack(limbWeaponMorph);
    }

    private void LimbHeavyAttack()
    {
     
      TryQueueAttack(limbWeaponMorph, false);

    }
    
    private void MouthLightAttack()
    {
        TryQueueAttack(headWeaponMorph);
    }

    private void MouthHeavyAttack()
    {
     
        TryQueueAttack(headWeaponMorph, false);

    }
    
    private void TailLightAttack()
    {
        TryQueueAttack(tailWeaponMorph);
    }

    private void TailHeavyAttack()
    {
     
        TryQueueAttack(tailWeaponMorph, false);

    }

    private void OnMorphLoadoutChanged(Morph weaponMorph)
    {
        if(weaponMorph == null) return;
        
        if (weaponMorph is LimbWeaponMorph limb)
        {
            limbWeaponMorph = limb;
        } else if (weaponMorph is HeadWeaponMorph head)
        {
            headWeaponMorph = head;
        }else if (weaponMorph is TailWeaponMorph tail)
        {
            tailWeaponMorph = tail;
        }
    }
}
