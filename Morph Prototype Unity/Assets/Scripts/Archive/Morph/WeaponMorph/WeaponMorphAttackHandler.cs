using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO - remove if is player input check
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

[RequireComponent(typeof(MorphLoadout))]
public class WeaponMorphAttackHandler : MonoBehaviour
{
    private MorphLoadout loadout;
    
    private LimbWeaponOutdatedMorph _limbWeaponOutdatedMorph;
    private HeadWeaponOutdatedMorph _headWeaponOutdatedMorph;
    private TailWeaponOutdatedMorph _tailWeaponOutdatedMorph;

    private BoxHitbox boxHitbox;
    private SphericalHitbox sphericalHitbox;

    private OutdatedWeaponAttack _currentOutdatedWeaponAttack;
    private WeaponOutdatedMorph _currentWeaponOutdatedMorph;
    private List<OutdatedWeaponAttack> attackQueue;
    private Timer attackTimer;
    private Timer inputWindowTimer;

    // Start is called before the first frame update
    void Awake()
    {
        loadout = GetComponent<MorphLoadout>();
        attackQueue = new List<OutdatedWeaponAttack>();
        inputWindowTimer = new Timer(0);

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
        if (gameObject.GetComponentInParent<PlayerCreatureCharacter>())
        {
            T_HandleInput();
        }

        InputWindowAfterAttack();
        if(attackQueue.Count < 1) return;
        ExecuteAttacks();
    }

    private void T_HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LimbLightAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LimbHeavyAttack();
        }
    }

    private void InputWindowAfterAttack()
    {
        if(attackQueue.Count > 0) return;
        if (!inputWindowTimer.IsFinished())
        {
            inputWindowTimer.CountDown(Time.deltaTime);
            

            if (inputWindowTimer.JustFinished)
            {
                ResetCombo();
            }
        }
    }

    private void ResetCombo()
    {
        _limbWeaponOutdatedMorph?.ResetCombo();
        _tailWeaponOutdatedMorph?.ResetCombo();
        _headWeaponOutdatedMorph?.ResetCombo();
    }

    private void ExecuteAttacks()
    {
        _currentOutdatedWeaponAttack = attackQueue[0];

        RestartTimerIfNecessary();
        
        if (attackTimer.JustStarted)
            StartAttack();

        if (attackTimer.CountDown(Time.deltaTime))
            UpdateAttack();
        
        if (attackTimer.JustFinished)
            FinishAttack();
    }

    private void RestartTimerIfNecessary()
    {
        if (attackTimer == null || attackTimer.IsFinished())
        {
            attackTimer = new Timer(_currentOutdatedWeaponAttack.Duration);
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
        var hitbox = GetAppropriateHitbox(_currentOutdatedWeaponAttack.Data.HitBoxType);
        if(hitbox) hitbox.Activate();
        
        _currentOutdatedWeaponAttack.OnStart();
    }
    
    private void OnAttackHit(OutdatedDamageHandler outdatedDamageHandler)
    {
        _currentOutdatedWeaponAttack.OnHit(outdatedDamageHandler);
    }

    private void UpdateAttack()
    {
        _currentOutdatedWeaponAttack.OnUpdate();
    }

    private void FinishAttack()
    {
        // call finish on attack
        _currentOutdatedWeaponAttack.OnFinish();
        
        //turn off hitbox
        var hitbox = GetAppropriateHitbox(_currentOutdatedWeaponAttack.Data.HitBoxType);
        if(hitbox) hitbox.Deactivate();
        
        //start input window and remove this attack from queue
        inputWindowTimer = new Timer(_currentOutdatedWeaponAttack.InputWindowAfterAttack);
        attackQueue.RemoveAt(0);
    }

    private void TryQueueAttack(WeaponMorphType morphType, WeaponAttackType attackType)
    {
        switch (morphType)
        {
           case WeaponMorphType.Limb:
               TryQueueAttack(_limbWeaponOutdatedMorph, attackType);
               break;
           case WeaponMorphType.Head:
               TryQueueAttack(_headWeaponOutdatedMorph, attackType);
               break;
           case WeaponMorphType.Tail:
               TryQueueAttack(_tailWeaponOutdatedMorph, attackType);
               break;
        }
    }

    private void TryQueueAttack(WeaponOutdatedMorph weaponOutdatedMorph, WeaponAttackType attackType)
    {
        if(weaponOutdatedMorph == null) return;
        if (!CanQueueNextAttack(weaponOutdatedMorph, attackType)) return;

        var currentAttack = weaponOutdatedMorph.GetCurrentAttack(attackType);
        if (currentAttack != null)
        {
            attackQueue.Add(currentAttack);
            weaponOutdatedMorph.AdvanceCombo(attackType);
        }
    }

    private bool CanQueueNextAttack(WeaponOutdatedMorph weaponOutdatedMorph, WeaponAttackType attackType)
    {
        if (_currentOutdatedWeaponAttack == null) return true;
        if (attackQueue.Count > 1) return false;
        
        if (attackTimer.CurrentTime <= _currentOutdatedWeaponAttack.InputWindowDuringAttack
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

    private void OnMorphLoadoutChanged(WeaponOutdatedMorph outdatedMorph)
    {
        if(outdatedMorph == null) return;
        
        if (outdatedMorph is LimbWeaponOutdatedMorph limb)
        {
            _limbWeaponOutdatedMorph = limb;
        } else if (outdatedMorph is HeadWeaponOutdatedMorph head)
        {
            _headWeaponOutdatedMorph = head;
        }else if (outdatedMorph is TailWeaponOutdatedMorph tail)
        {
            _tailWeaponOutdatedMorph = tail;
        }
    }

   
}
