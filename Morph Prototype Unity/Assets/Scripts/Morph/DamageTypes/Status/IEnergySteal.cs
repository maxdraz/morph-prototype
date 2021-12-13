using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnergySteal : IDamageType
{
    public float EnergyToSteal { get; set; }

}
