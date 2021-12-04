using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class OnHitEffectData : IDamageType
{
    public abstract object Clone();

}
