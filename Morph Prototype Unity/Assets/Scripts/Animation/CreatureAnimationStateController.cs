using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationStateController : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private WeaponMorphAttackHandler weaponMorphAttackHandler;  // change to attack handler eventually
    private Movement movement;
    private float attackAnimationLength;
    
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
        if (health)
        {
            health.Died += OnDied;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(IsWeaponAttack, true);
          //  var attackStateLength = animator.GetNextAnimatorStateInfo(0).IsName("WeaponAttack");
          //  print(attackStateLength);
            //var duration = 5;
            //  animator.SetFloat(WeaponAttackSpeed,duration * (1/attackStateLength));

        }
        
        if (movement)
        {
            SetMovementSpeed(movement.GetMoveSpeedNormalized());
        }
     
    }

    private void OnWeaponAttackStarted(ref WeaponAttack currentWeaponAttack)
    {
        animator.SetBool(IsWeaponAttack, true);
        animator.SetFloat(WeaponAttackSpeed, currentWeaponAttack.Duration);
        print(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnDied()
    {
        animator.SetBool(IsDead, true);
    }

    private void SetMovementSpeed(float movementSpeedNormalized)
    {
        animator.SetFloat(MovementSpeed, movementSpeedNormalized);
    }

    private float GetClipLengthByName(string clipName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        return 0;
    }
}
