using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public WeaponMorphData data;
    // Start is called before the first frame update
    void Start()
    {
        string s = "  Blurry, Spurs tickets TO MATRS  ";
        s = s.Replace(" ", string.Empty);
        
        print(s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
