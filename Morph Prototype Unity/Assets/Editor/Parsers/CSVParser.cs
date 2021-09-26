using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public abstract class CSVParser
{
   protected string CSVPath;
   protected string outputDirectoryPath;

   protected string[] allLines;
   
   
   
   public abstract void Parse();

   protected virtual bool PathExists(string path)
   {
      if (!File.Exists(path))
      {
         Debug.LogWarning("Path not found!\n" + path);
         return false;
      }
      else
      {
         Debug.Log("file found!");
         return true;
      }
   }
   
   protected void TryReadFile(string path)
   {
      if (!File.Exists(path))
      {
         Debug.LogWarning("Path not found!\n" + path);
         return;
      }
      else
      {
         allLines = File.ReadAllLines(path);
      }
   }

   protected bool FileReadSuccessfully()
   {
      return allLines.Length > 0;
   }

   protected void UpdateAssetDatabase(UnityEngine.Object obj, in string outputPath)
   {
      if (File.Exists(outputPath))
      {
         AssetDatabase.DeleteAsset(outputPath);
      }
      AssetDatabase.CreateAsset(obj, outputPath);
   }
   
   protected void ClearDirectoryContents(in string directoryPath)
   {
      var existingFiles = Directory.GetFiles(directoryPath);
      foreach (var file in existingFiles)
      {
         File.Delete(file);
      }
   }
   
  
}
