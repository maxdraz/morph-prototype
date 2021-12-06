using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationStateController : MonoBehaviour, IEventSubscriber
{
    private Animator animator;
    private Health health;
   [SerializeField] private WeaponMorphAttackHandler weaponMorphAttackHandler;  // change to attack handler eventually
    private Movement movement;
    
    private WeaponAttack currentWeaponAttack;
    
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsWeaponAttack = Animator.StringToHash("IsWeaponAttack");
    private static readonly int WeaponAttackSpeed = Animator.StringToHash("WeaponAttackSpeed");
    private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        weaponMorphAttackHandler = GetComponent<WeaponMorphAttackHandler>();
        movement = GetComponentInParent<Movement>();

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WeaponAttack") && animator.GetBool(IsWeaponAttack))
        {
            if (currentWeaponAttack != null)
            {
                var offset = Mathf.Max(0.01f, currentWeaponAttack.Duration - 0.5f);
                animator.SetFloat(WeaponAttackSpeed, 1/(offset * 1/animator.GetCurrentAnimatorStateInfo(0).length));
                animator.SetBool(IsWeaponAttack, false);
            }
        }
    }

    private void UpdateMovementState()
    {
        if (movement)
        {
            SetMovementSpeed(movement.GetMoveSpeedNormalized());
        }

    }

    private void OnWeaponAttackStarted(ref WeaponAttack weaponAttack)
    { 
        print("weapon attack started!");
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
    }
}
