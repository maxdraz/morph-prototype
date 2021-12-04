using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoisonDamage : IChemicalDamage
{
   public float PoisonDamage { get; set; }
}
