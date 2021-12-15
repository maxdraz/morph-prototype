using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaseousDischarge : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockToxicOverflow = true;

    [SerializeField]private float poisonGasSpawnRate;
    [SerializeField] private float poisonGasLifeTime;
    [SerializeField] Timer poisonGasSpawnCountdown;
    ObjectPooler gasPooler;
    [SerializeField] GameObject gasCloud;

    // Start is called before the first frame update
    void Start()
    {
        poisonGasSpawnCountdown = new Timer(poisonGasSpawnRate, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //poisonGasSpawnCountdown.CountDown(poisonGasSpawnRate);
        poisonGasSpawnCountdown.Update(Time.deltaTime);

        if (poisonGasSpawnCountdown.JustCompleted) 
        {
            Debug.Log("Timer completed");
            CreateGasCloud();
        }
    }

    private void OnDamageAboutToBeTaken(ref IDamageType damageType) 
    {
        if (damageType is IPhysicalDamage physicalDamage) 
        {
            if (physicalDamage.PhysicalDamageDealt >= damageHandler.Health.CurrentHealth / 25) 
            {
            //now we apply poison damage and knockback to the source of the damage
                
            }
        }
    }

    void CreateGasCloud() 
    {
        GameObject poisonGasCloud = ObjectPooler.Instance.GetOrCreatePooledObject(gasCloud);
        poisonGasCloud.transform.position = transform.position;
        poisonGasCloud.GetComponent<PoisonGasCloud>().lifetime = poisonGasLifeTime;
        poisonGasCloud.GetComponent<PoisonGasCloud>().sourceCreature = this.gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(float amountToAdd)
    {

    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            if (unlockToxicOverflow)
            {
                damageHandler.DamageAboutToBeTaken += OnDamageAboutToBeTaken;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            if (unlockToxicOverflow)
            {
                damageHandler.DamageAboutToBeTaken -= OnDamageAboutToBeTaken;
            }

        }

        damageHandler = null;
    }
}
