using UnityEngine;
using UnityEditor;

public static class CustomTools 
{
    [MenuItem("Custom Tools/Rebuild Morph Database %F1")]
    static void RebuildMorphDatabase()
    {
        CsvManager.morphCsvMetadata.VerifyPath();
        //Debug.Log("Rebuilding morph database");
    }
}
