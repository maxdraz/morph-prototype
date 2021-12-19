using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationStateController : MonoBehaviour, IEventSubscriber
{
    private Animator animator;
    private Health health;
    private WeaponMorphAttackHandler weaponMorphAttackHandler;  // change to attack handler eventually
    private Movement movement;
    private DamageHandler damageHandler;
    
    private WeaponAttack currentWeaponAttack;
    
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsWeaponAttack = Animator.StringToHash("IsWeaponAttack");
    private static readonly int WeaponAttackSpeed = Animator.StringToHash("WeaponAttackSpeed");
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    private static readonly int DamageTaken = Animator.StringToHash("DamageTaken");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        weaponMorphAttackHandler = GetComponent<WeaponMorphAttackHandler>();
        movement = GetComponentInParent<Movement>();
        damageHandler = GetComponent<DamageHandler>();

    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeToEventsCoroutine());
    }

    private void OnDisable()
    {
       UnsubscribeFromEvents();
    }


    void Update()
    {
        UpdateMovementState();
        UpdateAttackSpeed();
    }

    private void UpdateAttackSpeed()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WeaponAttack"))
        {
            if (currentWeaponAttack != null)
            {
                var offset = Mathf.Max(0.01f, currentWeaponAttack.Duration * 0.8f);
                animator.SetFloat(WeaponAttackSpeed, 1/(offset * 1/animator.GetCurrentAnimatorStateInfo(0).length));
              //  animator.SetBool(IsWeaponAttack, false);
            }
        }
    }

    private void UpdateMovementState()
    {
        if (movement)
        {
            SetMovementSpeed(movement.MovementSpeedNormalized);
        }

    }

    private void OnWeaponAttackStarted(ref WeaponAttack weaponAttack)
    {
        animator.SetBool(IsWeaponAttack, true);
        currentWeaponAttack = weaponAttack;
    }

    private void OnDied()
    {
        animator.SetBool(IsDead, true);
    }

    private void SetMovementSpeed(float movementSpeedNormalized)
    {
        animator.SetFloat(MovementSpeed, movementSpeedNormalized);
    }

    private void OnDamageTaken(in DamageTakenSummary damageTakenSummary)
    {
        animator.SetBool(DamageTaken, true);
    }

    public IEnumerator SubscribeToEventsCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (health)
        {
            health.Died += OnDied;
        }

        if (weaponMorphAttackHandler)
        {
            weaponMorphAttackHandler.AttackHasStarted += OnWeaponAttackStarted;
        }

        if (damageHandler)
        {
            damageHandler.DamageHasBeenTaken += OnDamageTaken;
        }
    }

    public void UnsubscribeFromEvents()
    {
        if (health)
        {
            health.Died -= OnDied;
        }
        
        if (weaponMorphAttackHandler)
        {
            weaponMorphAttackHandler.AttackHasStarted -= OnWeaponAttackStarted;
        }
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenTaken -= OnDamageTaken;
        }
    }
}
