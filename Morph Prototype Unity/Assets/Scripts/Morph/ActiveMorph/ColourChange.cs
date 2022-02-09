using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : ActiveMorph
{
    static int stealthPrerequisit = 200;
    static int intelligencePrerequisit = 50;

    private DamageHandler damageHandler;
    Stats stats;
    float range;
    Stealth stealth;
    [SerializeField] private int stealthStatBonus = 5;


    [SerializeField] private int colourChangeStealthBonus;
    [SerializeField] private float colourChangeDuration;

    [SerializeField] private bool unlockShimmering = true;
    [SerializeField] private float shimmeringPerceptionReduction;
    [SerializeField] private GameObject shimmeringParticles;
    ParticleSystem shimmering;

    [SerializeField] private bool unlockBioLuminescentFlash = true;
    [SerializeField] private GameObject bioluminescentFlash;
    [SerializeField] private int perceptionDamage;
    [SerializeField] private float blindnessDuration;


    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {
        new Prerequisite("stealth", stealthPrerequisit),
        new Prerequisite("intelligence", intelligencePrerequisit)
    };


    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStealthStat(stealthStatBonus);

        if (unlockShimmering) 
        {
            shimmering = ObjectPooler.Instance.GetOrCreatePooledObject(shimmeringParticles).GetComponent<ParticleSystem>();
            shimmering.transform.position = transform.position;
        }
    }

    private void Update()
    {
        if (cooldown.JustCompleted) 
        {
            shimmering.Play();
        }

        if (Input.GetKeyDown(testInput)) 
        {
            Active();
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
        ObjectPooler.Instance.GetOrCreatePooledObject(bioluminescentFlash);
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
