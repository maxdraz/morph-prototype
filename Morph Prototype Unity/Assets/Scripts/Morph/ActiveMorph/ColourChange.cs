using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : ActiveMorph
{
    
    [SerializeField] private int stealthStatBonus = 5;
    [SerializeField] private int colourChangeStealthBonus;
    [SerializeField] private float colourChangeDuration;

    [SerializeField] private bool unlockShimmering = true;
    [SerializeField] private float shimmeringPerceptionReduction;
    [SerializeField] private GameObject shimmeringParticles;
    ParticleSystem shimmering;

    [SerializeField] private bool unlockBioLuminescentFlash = true;
    [SerializeField] private GameObject bioluminescentFlash;
    [SerializeField] private List<OnHitEffectDataContainer> onHitEffects;
    
    private float range;
    private Stealth stealth;
    
    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        stealth = GetComponent<Stealth>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        if (unlockShimmering)
        {
            shimmering = ObjectPooler.Instance.GetOrCreatePooledObject(shimmeringParticles).GetComponent<ParticleSystem>();
            shimmering.transform.position = transform.position;
            shimmering.transform.parent = transform;
        }
        
        ChangeStealthStat(stealthStatBonus);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeStealthStat(-stealthStatBonus);
    }

    protected override void Update()
    {
        base.Update();
        
        if (cooldown.JustCompleted && unlockShimmering) 
        {
            shimmering.Play();
        }

        if (Input.GetKeyDown(testInput)) 
        {
            Active();
            SpendEnergy(energyCost);
            SpendStamina(staminaCost);
        }
    }

    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private void OnDamageAboutToBeTaken(ref IDamageType damageType)
    {
        //incoming damage needs to overcome shimmeringMissChance
        //shimmeringMissChance = target perception / shimmeringPerceptionReduction;
    }

    public void Active() 
    {
        //This morphs has 2 actives, 1 in stealthmode and another not in stealthmode. They share the same cooldown
        SpendEnergy(energyCost);
        SpendStamina(staminaCost);

        if (unlockShimmering) 
        {
            shimmering.Stop();
        }

        if (stealth.stealthMode) 
        {
            StartCoroutine("ColourChangeActive");
        } 
        else 
        {
            if (unlockBioLuminescentFlash) 
            {
                BioluminescentFlash();
            }
        }
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            Active();
            return true;
        }

        return false;
    }

    IEnumerator ColourChangeActive()
    {
        stealth.flatStealthModifier += colourChangeStealthBonus;
        yield return new WaitForSeconds(colourChangeDuration);

        stealth.flatStealthModifier -= colourChangeStealthBonus;
        yield return null;
    }

    private void BioluminescentFlash()
    {
        GameObject BioluminescentFlash = ObjectPooler.Instance.GetOrCreatePooledObject(bioluminescentFlash);
        BioluminescentFlash.transform.position = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            var otherDamageHandler = hitCollider.gameObject.GetComponentInChildren<DamageHandler>();

            foreach (var onHitEffectDataContainer in onHitEffects)
            {
                onHitEffectDataContainer.OnHitEffect.ApplyOnHitEffect(onHitEffectDataContainer.Data, otherDamageHandler, GetComponent<DamageHandler>());
                //print("should be applying damage");
                // otherDamageHandler.ApplyDamage(onHitEffectDataContainer.Data, damageDealer);

            }
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();

        if (!damageHandler) return;
        if (unlockShimmering)
            damageHandler.DamageAboutToBeTaken += OnDamageAboutToBeTaken;
        
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();

        if (!damageHandler) return;
        if (unlockShimmering)
            damageHandler.DamageAboutToBeTaken -= OnDamageAboutToBeTaken;
        
    }

    private void OnValidate()
    {
        if (onHitEffects == null || onHitEffects.Count <= 0) return;
        
        foreach (var onHitEffectDataContainer in onHitEffects)
        {
            onHitEffectDataContainer.OnValidate();
        }
    }
}
