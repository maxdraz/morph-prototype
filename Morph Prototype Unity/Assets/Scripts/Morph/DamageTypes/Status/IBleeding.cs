using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBleeding : IDamageType
{
    public int BleedValue { get; set; }
}
