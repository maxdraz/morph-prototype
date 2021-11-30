using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    private Stats stats;

    public float CurrentHealth=> health;
    public event Action Died;
    public event Action<float> HealthChanged;

    
    // Start is called before the first frame update
    void Awake()
    {
        //get stats component
        stats = GetComponent<Stats>();
        health = stats ? stats.MaxHealth : 100;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractHP(float amount)
    {
        health -= amount;
        health = Mathf.Max(0, health);
        
        HealthChanged?.Invoke(health);
        
        if (health <= 0)
        {
            Die();
        }
        
        
    }

    public void AddHP(float amount)
    {
        
    }

    

    private void Die()
    {
        Destroy(transform.parent.gameObject);
        Died?.Invoke();
    }
}
