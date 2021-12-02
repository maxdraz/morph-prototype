using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class OutdatedDebuff
{
    public abstract void OnUpdate(OutdatedDamageHandler outdatedDamageTaker, float dt);
    public abstract bool IsFinished();
    public abstract void ApplyDebuff(OutdatedDamageHandler outdatedDamageTaker);

}
