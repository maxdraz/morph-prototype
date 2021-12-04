using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;

// TODO - run fortitude test in ApplyDebuff ??

public class OutdatedDamageHandler : MonoBehaviour
{
    private Stats stats;
    private Health health;
    private Armor armor;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private DamageNumber damageNumber;

    public Stats Stats => stats;
    public Health Health => health;
    public Armor Armor => armor;
    public bool CanTakeDamage => canTakeDamage;

    [SerializeField] private List<OutdatedDebuff> activeDebuffs;
    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponentInParent<Stats>();
        health = GetComponent<Health>();
        armor = GetComponent<Armor>();
        activeDebuffs = new List<OutdatedDebuff>();
        
        
        if(!stats) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find stats");
        if(!health) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find health");
    }

    // Update is called once per frame
    void Update()
    {
        // run debuffs if any in list
        if(activeDebuffs.Count <= 0) return;
        ApplyActiveDebuffs();
    }

    private void ApplyActiveDebuffs()
    {
        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            var currentDebuff = activeDebuffs[i];
            if (!currentDebuff.IsFinished())
            {
                print(transform.name + " updating poison dot");
                currentDebuff.OnUpdate(this, Time.deltaTime);
                continue;
            }
            print(transform.name + "poison dot finished");
            activeDebuffs.RemoveAt(i--);
        }
    }

    public void ApplyDebuff(OutdatedDebuff outdatedDebuff)
    {
        if(canTakeDamage) 
            activeDebuffs.Add(outdatedDebuff);
    }
    
    // void take dmaage (  )

    public float ApplyDamage(float damage, OutdatedDamageType outdatedDamageType)
    {
        if (canTakeDamage)
        {
            damage = ApplyResistances(damage, outdatedDamageType);
            health.SubtractHP(damage);
            return damage;

            // we can instantiate a 'DamageReport' gameobject here and pass in damage and damagetype to the 'DamageReport' script component
        }

        return 0;
    }

    private float ApplyResistances(float damage, OutdatedDamageType outdatedDamageType)
    {
        switch (outdatedDamageType)
        {
            case OutdatedDamageType.Poison:
                return DamageFormulas.ElementalDamageResist(damage, stats.PoisonResistance, 0, 0);
                break;
            case OutdatedDamageType.PhysicalNormal:
                return DamageFormulas.PhysicalDamageResist(damage,false,stats.ToughnessModifier,0, armor.HasArmor, 0);
                break;
            default:
                return 0;
           
        }
    }

    
}
