using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockUp : IDamageType
{
    public float KnockupForce { get; set; }

}
