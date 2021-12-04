using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageHandler))]
public class DebuffHandler : MonoBehaviour
{
    private DamageHandler damageHandler;
   [SerializeField] private PoisonDebuff poisonDebuff;
   private AcidDebuff acidDebuff;
    [SerializeField] private List<Debuff> activeDebuffs;

    // Start is called before the first frame update
    void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();

        activeDebuffs = new List<Debuff>();
        poisonDebuff = new PoisonDebuff(new Timer(1,true));
        acidDebuff = new AcidDebuff(new Timer(1, true));
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
        
        if(activeDebuffs.Count <= 0) return;

        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            var debuff = activeDebuffs[i];
            debuff.TickTimer.CountDown(Time.deltaTime);

            if (debuff.TickTimer.JustFinished)
            {
                var damageType = debuff.GetTickDamage();
                if (damageType is IPoisonDamage psn)
                {
                    print(psn.PoisonDamage);
                }
                damageHandler.ApplyDamage(damageType, debuff.DamageDealer);
            }

            if (debuff.IsFinished())
            {
                activeDebuffs.RemoveAt(i--);
            }
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
                    activeDebuffs.Add(poisonDebuff);
                }
                
                print("poison damage to be dealt: " + poisonDamage.PoisonDamage);
                poisonDebuff.AddDebuffContributor(damageDealer,poisonDamage.PoisonDamage);
            }
        }

        if (damageType is IAcidDamage acidDamage)
        {
            if (acidDamage.AcidDamage > 0)
            {
                if (acidDebuff.IsFinished())
                {
                    activeDebuffs.Add(acidDebuff);
                }
                
                
            }
        }
        
    }
}