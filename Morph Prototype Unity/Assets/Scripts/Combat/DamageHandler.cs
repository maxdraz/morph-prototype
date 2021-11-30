using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - run fortitude test in ApplyDebuff ??

public class DamageHandler : MonoBehaviour
{
    private Stats stats;
    private Health health;

    public Stats Stats => stats;

    [SerializeField] private List<Debuff> activeDebuffs;
    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponentInParent<Stats>();
        health = GetComponent<Health>();
        activeDebuffs = new List<Debuff>();
        
        
        if(!stats) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find stats");
        if(!health) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find health");
    }

    // Update is called once per frame
    void Update()
    {
        // run debuffs if any in list
        if(activeDebuffs.Count <= 0) return;
        ApplyActiveDebuffs();
    }

    private void ApplyActiveDebuffs()
    {
        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            var currentDebuff = activeDebuffs[i];
            if (!currentDebuff.IsFinished())
            {
                print(transform.name + " updating poison dot");
                currentDebuff.OnUpdate(this, Time.deltaTime);
                continue;
            }
            print(transform.name + "poison dot finished");
            activeDebuffs.RemoveAt(i--);
        }
    }

    public void ApplyDebuff(Debuff debuff)
    {
        activeDebuffs.Add(debuff);
    }
    
    // void take dmaage (  )

    public void ApplyDamage(float damage)
    {
       health.SubtractHP(damage);
    }
}
