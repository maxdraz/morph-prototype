using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageHandler))]
public class DebuffHandler : MonoBehaviour
{
    private DamageHandler damageHandler;
   [SerializeField] private PoisonDebuff poisonDebuff;
   [SerializeField] private AcidDebuff acidDebuff;
    [SerializeReference] private List<Debuff> debuffs;
    [SerializeReference] private List<PhysicsDebuff> physicsDebuffs;

    private float poisonStack;
    private float acidStack;

    public float PoisonStack => poisonStack;
    public float AcidStack => acidStack;


    // Start is called before the first frame update
    void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();

        debuffs = new List<Debuff>();
        physicsDebuffs = new List<PhysicsDebuff>();
        poisonDebuff = new PoisonDebuff(new LegacyTimer(1,true));
        acidDebuff = new AcidDebuff(new LegacyTimer(5),new LegacyTimer(1, true));
    }

    private void OnEnable()
    {
        damageHandler.DebuffAboutToBeTakenPostModifier += OnDebuffAboutToBeTakenPostModifier;
    }

    private void OnDisable()
    {
        damageHandler.DebuffAboutToBeTakenPostModifier -= OnDebuffAboutToBeTakenPostModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            damageHandler.ApplyDebuff(new PoisonDamageData(2000), damageHandler);
        }
        
        ProcessDebuffs();
    }

    private void FixedUpdate()
    {
        ProcessPhysicsDebuffs();
    }

    public void AddPhysicsDebuff(PhysicsDebuff debuff)
    {
        physicsDebuffs.Add(debuff);
    }

    private void ProcessDebuffs()
    {
        if(debuffs.Count <= 0) return;

        for (int i = 0; i < debuffs.Count; i++)
        {
            var debuff = debuffs[i];
            debuff.CountdownTimer(Time.deltaTime);

            if (debuff.ShouldTick())
            {
                var damageType = debuff.GetTickDamage();
                if (damageType is IPoisonDamage psn)
                {
                    
                }
                damageHandler.ApplyDamage(damageType, debuff.DamageDealer);
            }

            if (debuff.IsFinished())
            {
                debuffs.RemoveAt(i--);
            }
        }
    }

    private void ProcessPhysicsDebuffs()
    {
        for (int i = 0; i < physicsDebuffs.Count; i++)
        {
            var debuff = physicsDebuffs[i];
            debuff.OnFixedUpdate(Time.deltaTime);
            
            if(debuff.CheckIfFinished())
                physicsDebuffs.RemoveAt(i--);
        }
    }

    private void OnDebuffAboutToBeTakenPostModifier(ref IDamageType damageType, DamageHandler damageDealer)
    {
        if (damageType is IPoisonDamage poisonDamage)
        {
            if (poisonDamage.PoisonDamage > 0)
            {
                if (poisonDebuff.IsFinished())
                {
                    debuffs.Add(poisonDebuff);
                }
                poisonDebuff.AddDebuffContributor(damageDealer,poisonDamage.PoisonDamage);
            }
        }

        if (damageType is IAcidDamage acidDamage)
        {
            if (acidDamage.AcidDamage > 0)
            {
                if (acidDebuff.IsFinished())
                {
                    acidDebuff.OnStart(acidDamage.AcidDOTDuration);
                    debuffs.Add(acidDebuff);
                    
                }
                
                // add 20 percent to stack
                var acidDamageToStack = acidDamage.AcidDamage * 0.2f;
                acidDebuff.AddDebuffContributor(damageDealer, acidDamageToStack, acidDamage.AcidDOTDuration);
                //take 80 percent
                acidDamage.AcidDamage *= 0.8f;
                damageHandler.ApplyDamage(acidDamage, damageDealer);
                damageHandler.ApplyDamage(acidDebuff.GetTickDamage(), damageDealer);
            }
        }
    }

    public void StopAllDebuffs()
    {
        debuffs.Clear();
    }
}
