using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Debuff
{
    public abstract void OnUpdate(DamageHandler damageTaker, float dt);
    public abstract bool IsFinished();

}
