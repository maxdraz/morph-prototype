using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    [SerializeField] private List<Timer> timers;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        timers = new List<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timers.Count < 1) return;
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        for (int i = 0; i < timers.Count; i++)
        {
           // timers[i].IsFinished()
        }
    }

    private void CreateInstance()
    {
        var managerGO = new GameObject("TimerManager");
        managerGO.AddComponent(typeof(TimerManager));
       
    }

    private void AddTimer(ref Timer t)
    {
        while (true)
        {
            if (!Instance)
            {
                CreateInstance();
                continue;
            }
            else
            {
                timers.Add(t);
            }

            break;
        }
    }

    private void RemoveTimer(int index)
    {
        while (true)
        {
            if (!Instance)
            {
                CreateInstance();
                continue;
            }
            else
            {
                if (timers.Count > 1
                    && index >= 0
                    && index < timers.Count)
                {
                    timers.RemoveAt(index);
                }
            }
            
            break;
        }
        
    }
}
