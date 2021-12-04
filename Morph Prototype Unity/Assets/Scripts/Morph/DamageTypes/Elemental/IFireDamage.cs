using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireDamage : IElementalDamage
{
    public float FireDamage { get; set; }
}
