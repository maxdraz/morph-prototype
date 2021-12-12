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
    [SerializeReference] private List<Debuff> activeDebuffs;

    // Start is called before the first frame update
    void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();

        activeDebuffs = new List<Debuff>();
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
        
        if(activeDebuffs.Count <= 0) return;

        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            var debuff = activeDebuffs[i];
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
                    activeDebuffs.Add(acidDebuff);
                    
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
        activeDebuffs.Clear();
    }
}
