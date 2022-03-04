using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullTowards : IDamageType
{
    public float PullForce { get; set; }
}
