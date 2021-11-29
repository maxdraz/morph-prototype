using UnityEngine;
using UnityEditor;
using System.IO;

public static class CSVToSOConverter
{
    [MenuItem("Utilities/CSV/Rebuild Morph Database/Weapon Morphs")]
    public static void RebuildWeaponMorphs()
    {
       // var parser = new WeaponMorphCSVParser();
     //   Rebuild(parser);
    }
    public static void Rebuild(CSVParser parser)
    {
        parser.Parse();
    }
    
    [MenuItem("Utilities/CSV/Rebuild Morph Database/All")]
    public static void RebuildAll()
    {
        RebuildWeaponMorphs();
    }
}
