using UnityEngine;
using System.IO;

public static class CsvManager
{
    public static MorphCsvMetadata morphCsvMetadata = new MorphCsvMetadata();

    public static CsvMetadata[] CsvMetadataArray = new CsvMetadata[]
    {
        morphCsvMetadata
    };


    //private static string separator = ",";

    //Morph data
    // private static string morphCSVPath = "Data/CSV/Morphs/MorphData.csv";
    //private static string morphSOPath = "ScriptableObjects/Morphs";
    //private static 

    //static void VerifyPath()
    // {
    // if(!Directory.Exists(morphCSVPath))
    //}
}
