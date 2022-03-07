using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : ActiveMorph
{
    static int stealthPrerequisit = 200;
    static int intelligencePrerequisit = 50;

    private DamageHandler damageHandler;
    public Stats stats;
    float range;
    public Stealth stealth;
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

    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("stealth", stealthPrerequisit),
        new Prerequisite("intelligence", intelligencePrerequisit)
    };

    private void Start()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();

        if (unlockShimmering)
        {
            shimmering = ObjectPooler.Instance.GetOrCreatePooledObject(shimmeringParticles).GetComponent<ParticleSystem>();
            shimmering.transform.position = transform.position;
            shimmering.transform.parent = transform;
        }
    }

    private void OnEnable()
    {
        
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStealthStat(stealthStatBonus);

        
    }

    private void Update()
    {
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

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeStealthStat(-stealthStatBonus);
    }

    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
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

    

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            if (unlockShimmering) 
            {
                damageHandler.DamageAboutToBeTaken += OnDamageAboutToBeTaken;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

            if (unlockShimmering)
            {
                damageHandler.DamageAboutToBeTaken -= OnDamageAboutToBeTaken;
            }
        }

        damageHandler = null;
    }
}
