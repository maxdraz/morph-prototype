using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStaminaSteal : IDamageType
{
    public float StaminaToSteal { get; set; }
}
