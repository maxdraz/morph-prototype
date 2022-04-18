using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullTowards : IDamageType
{
    public Transform PullTowardsThis { get;}
    public float PullForce { get; }
    public ForceMode ForceMode { get;}
}
