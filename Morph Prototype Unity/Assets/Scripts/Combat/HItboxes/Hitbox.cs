using System;
using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    protected Collider col;

    public event Action Hit;
    public event Action HitContinuous;
    
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        
        StopDetecting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDetecting()
    {
        col.enabled = true;
    }

    public void StopDetecting()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Hit?.Invoke();
                                                                // TODO
                                                                // transmit enemy gameobject?
                                                                // use layer masks instead of tags?
        }
    }
}
