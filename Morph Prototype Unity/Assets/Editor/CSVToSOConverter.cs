using UnityEngine;
using UnityEditor;
using System.IO;

public static class CSVToSOConverter
{
    [MenuItem("Utilities/CSV/Rebuild Scriptable Objects/Weapon Morphs")]
    public static void RebuildWeaponMorphs()
    {
        var parser = new WeaponMorphCSVParser();
        Rebuild(parser);
    }
    public static void Rebuild(CSVParser parser)
    {
        parser.Parse();
    }
    
    [MenuItem("Utilities/CSV/Rebuild Scriptable Objects/All")]
    public static void RebuildAll()
    {
        RebuildWeaponMorphs();
    }
}
