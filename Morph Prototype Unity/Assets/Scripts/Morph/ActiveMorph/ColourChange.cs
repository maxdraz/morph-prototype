using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : ActiveMorph
{
    private DamageHandler damageHandler;
    Stats stats;
    float range;
    Stealth stealth;
    [SerializeField] private int stealthStatBonus = 5;


    [SerializeField] private int colourChangeStealthBonus;
    [SerializeField] private float colourChangeDuration;

    [SerializeField] private bool unlockShimmering = true;
    [SerializeField] private float shimmeringPerceptionReduction;

    [SerializeField] private bool unlockBioLuminescentFlash = true;
    [SerializeField] private int perceptionDamage;
    [SerializeField] private float blindnessDuration;




    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStealthStat(stealthStatBonus);
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
        //This morphs has 2 actives, 1 in stealthmode and another not in stealthmode. They  share the same cooldown
        if (stealth.stealthMode) 
        {
            StartCoroutine("ColourChangeActive");
        } 
        else 
        {
            BioluminescentFlash();
        }
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Perception>() && hitCollider.gameObject != gameObject)
            {
                hitCollider.GetComponent<Fortitude>().ReduceFortitude(perceptionDamage, "blindness", blindnessDuration);
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
