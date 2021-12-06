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
   [SerializeField] private DamageHandler damageHandler;
    
    [SerializeField] private LimbWeaponMorph limbWeaponMorph;
    private HeadWeaponMorph headWeaponMorph;
    private TailWeaponMorph tailWeaponMorph;

    private BoxHitbox boxHitbox;
    private SphericalHitbox sphericalHitbox;

    private OutdatedWeaponAttack _currentOutdatedWeaponAttack;
    private WeaponOutdatedMorph _currentWeaponOutdatedMorph;
    private List<OutdatedWeaponAttack> outdatedAttackQueue;

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
        damageHandler = GetComponent<DamageHandler>();
        outdatedAttackQueue = new List<OutdatedWeaponAttack>();
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
        if(limbWeaponMorph)
            limbWeaponMorph.ResetCombo();
        
        if(tailWeaponMorph)
            tailWeaponMorph.ResetCombo();
        
        if(tailWeaponMorph)
            headWeaponMorph.ResetCombo();
    }

    private void ExecuteAttacks()
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
        print("attack has started and event sent!!!");
        AttackHasStarted?.Invoke(ref currentWeaponAttack);
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

    // private void TryQueueAttack(WeaponMorphType morphType, WeaponAttackType attackType)
    // {
    //     switch (morphType)
    //     {
    //        case WeaponMorphType.Limb:
    //            TryQueueAttack(outdatedLimbWeaponMorph, attackType);
    //            break;
    //        case WeaponMorphType.Head:
    //            TryQueueAttack(headWeaponMorph, attackType);
    //            break;
    //        case WeaponMorphType.Tail:
    //            TryQueueAttack(tailWeaponMorph, attackType);
    //            break;
    //     }
    // }

    // private void TryQueueAttack(WeaponOutdatedMorph weaponOutdatedMorph, WeaponAttackType attackType)
    // {
    //     if(weaponOutdatedMorph == null) return;
    //     if (!CanQueueNextAttack(weaponOutdatedMorph, attackType)) return;
    //
    //     var currentAttack = weaponOutdatedMorph.GetCurrentAttack(attackType);
    //     if (currentAttack != null)
    //     {
    //         attackQueue.Add(currentAttack);
    //         weaponOutdatedMorph.AdvanceCombo(attackType);
    //     }
    // }

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

    public void LimbLightAttack()
    {
       // TryQueueAttack(WeaponMorphType.Limb, WeaponAttackType.Light);
       TryQueueAttack(limbWeaponMorph);
    }

    public void LimbHeavyAttack()
    {
      //  TryQueueAttack(WeaponMorphType.Limb, WeaponAttackType.Heavy);
      TryQueueAttack(limbWeaponMorph, false);

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
