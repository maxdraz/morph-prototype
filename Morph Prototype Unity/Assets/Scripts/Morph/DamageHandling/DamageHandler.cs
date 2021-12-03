using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageHandler : MonoBehaviour
{

    public float health = 100;

    public DamageTakenSummary summary;
    //==============================
    [SerializeField] private List<Debuff> activeDebuffs;
    private List<DamageHandler> damageTakers;

    
    // public events
    public delegate void DamageAboutToBeTakenHandler(ref IDamageType damageType);
    public event DamageAboutToBeTakenHandler DamageAboutToBeTaken;
    public delegate void DamageHasBeenTakenHandler(in DamageTakenSummary damageSummary);
    public event DamageHasBeenTakenHandler DamageHasBeenTaken;
    public event Action<DamageHandler> DamageTakerAdded;
    public event Action<DamageHandler> DamageTakerRemoved;

    private void Update()
    {
        // tick debuffs
    }

    public void ApplyDamage(IDamageType damage)
    {
        //TODO make copy of damage and broadcast
         var damageClone = damage.Clone() as IDamageType;
        // DamageAboutToBeDealt.?Invoke( ref damageCopy )
        DamageAboutToBeTaken?.Invoke(ref damageClone);
        //  ResistDamage( ref damageCopy )
        DamageTakenSummary damageTakenSummary;
        ResistDamage(ref damageClone, out damageTakenSummary);
        // health -= damageTakenSummary. totalDamage
        summary = damageTakenSummary;
        print("total damage done:" +  damageTakenSummary.TotalDamage);
        DamageHasBeenTaken?.Invoke(in damageTakenSummary);
            //  DamageHasBeenDealt ( DamageDealtInfo )
    }

    public void ApplyDebuff(Debuff debuff)
    {
        // make copy of debuff
        // broadcast DebuffAboutToBeApplied ( debuff copy)
    }
    private void ResistDamage(ref IDamageType damage, out DamageTakenSummary damageTakenSummary)
    {
        damageTakenSummary = new DamageTakenSummary();
        
        if (damage is IPhysicalDamage)
        {
            print("is subclass");
            bool isPiercing = damage is IPiercingDamage;
            bool isImpact = damage is IImpactDamage;
            
            // float trueDamage = PhysDamageFormula
            // damageDeltInfo. physicalDamage += true damage
            if (damage is IPhysicalDamage physicalDamage)
            {
                print("morph damage = " + physicalDamage.MorphDamage);
                var damageDealt = physicalDamage.StrikeModifier + physicalDamage.MorphDamage;
                health -= damageDealt;
                damageTakenSummary.PhysicalDamage += damageDealt;
            }
           
        }
        
        // if (typeof(damage.GetType()))
        // {
        //     if(physicalDamage is IPiercingDamage)
        //     // var trueDamage = DamageCalculator.PhysicalResist(physicalDamage)
        // }
    }
}
