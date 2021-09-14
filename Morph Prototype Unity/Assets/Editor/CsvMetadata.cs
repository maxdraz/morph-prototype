using UnityEngine;
using System.IO;
using PlasticGui.Help.Conditions;

public abstract class CsvMetadata
{
    protected string CSVDirectoryPath { get; set; }
    protected string CSVFilename;
    protected string outputDirectoryPath { get; set; }

    public void VerifyPath()
    {
        if (!Directory.Exists(CSVDirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + "/" + CSVDirectoryPath);
            Debug.Log("Directory created: " + CSVDirectoryPath);
        }

        if (!Directory.Exists(CSVDirectoryPath + "/"+ CSVFilename))
        {
            Debug.LogWarning(CSVFilename + " not found!");
        }
    }
}
