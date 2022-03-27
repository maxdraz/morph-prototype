using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_IsMorphTypeTest : MonoBehaviour
{

    public MorphLoadout loadout;
    
    // Start is called before the first frame update
    void Start()
    {
        print(loadout.IsMorphEquipped<BrutalSpurs>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
