using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private HitboxManager hitboxManager;

    [SerializeField] private Hitbox sphericalHitbox;
    
    //damage formula test
    private float weaponMorphDamage = 15;
    private float meleeDamageStat = 30;
    private float currentAttackDamage = 4;
    private float bonusDamage = 10;
    private float flatBonusDamage = 5;
    
    private void Awake()
    {
        hitboxManager = GetComponent<HitboxManager>();
    }

    private void Start()
    {
      
    }

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackStart();
        }

        if (Input.GetMouseButtonDown(1))
        {
           AttackEnd();
        }
    }

        void AttackStart()
        {
            if (!sphericalHitbox)
            {
                sphericalHitbox = hitboxManager.GetHtibox<CubeHitbox>();
            }

            if (!sphericalHitbox)
                return;

            sphericalHitbox.Hit += OnHit;
            sphericalHitbox.StartDetecting();
                    
            
        }

        void AttackEnd()
        {
            if (!sphericalHitbox)
                return;
            
            sphericalHitbox = hitboxManager.GetHtibox<CubeHitbox>();
            sphericalHitbox.Hit -= OnHit;
            sphericalHitbox.StopDetecting();
            
        }

        void OnHit()
        {
            float potentialDamage = Damage.PhysicalDamage(weaponMorphDamage, meleeDamageStat, currentAttackDamage,
                bonusDamage, flatBonusDamage);
            print(potentialDamage + " potential damage dealt");
        }

        void OnHitEnd()
        {
            
        }
}
