using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStaminaDrain : IDamageType
{
    public float StaminaToDrain { get; set; }
}
