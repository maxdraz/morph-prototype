using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;

public class WeaponMorphCSVParser : CSVParser
{
    private string attackDataDirectoryPath; 
    
    public WeaponMorphCSVParser()
    {
        CSVPath = Application.dataPath + "/Editor/CSV/WeaponMorphs.csv";
        outputDirectoryPath = "Assets/Scripts/Morphs/WeaponMorphs/Data";
        attackDataDirectoryPath = outputDirectoryPath + "/AttackData";

        TryReadFile(CSVPath);
    }

    public override void Parse()
    {
        if (!FileReadSuccessfully()) return;


        //skip first line as its just headers
        for (int i = 1; i < allLines.Length; i++)
        {
            // get words in line
            var word = allLines[i].Split(',');
            int currentWordIndex = 0;
            //create SO
            var data = ScriptableObject.CreateInstance<WeaponMorphData>();
            //assign morph and SO name
            data.morphName = word[currentWordIndex++];
            //process creature list
            
            List<string> creatureTypes = new List<string>();

            if (word[currentWordIndex].StartsWith(("\"")))
            {
                var shouldLoop = true;
                while (shouldLoop)
                {
                    var currentCreature = word[currentWordIndex].Trim('"', ' ');

                    creatureTypes.Add(currentCreature);
                    shouldLoop = !word[currentWordIndex].EndsWith("\"");
                    currentWordIndex++;
                }
            }
            else
            {
                creatureTypes.Add(word[currentWordIndex]);
                
            }

            data.creatures = new CreatureType[creatureTypes.Count];
            var creatureNames = Enum.GetNames(typeof(CreatureType));
            //compare if creatures are valid
            for (int x = 0; x< creatureTypes.Count; x++)
            {
                for (int j = 0; j < creatureNames.Length; j++)
                {
                    // if creature type exists
                    if(creatureTypes[x] == creatureNames[j]){
                        // add it to list
                        CreatureType creatureType;
                        if (Enum.TryParse(creatureTypes[x], true, out creatureType))
                        {
                            data.creatures[x]= creatureType;
                        }
                        else
                        {
                            // creature type doesn't exist, error
                            Debug.LogError("Creature type doesn't exist \nexcel location:\nrow = " + i +1 + "\nword = " + creatureTypes[x]);
                        }

                    }
                }
            }

            //var basicAttack = ScriptableObject.CreateInstance<WeaponMorphAttackData>();
            //data.basicAttackData = new WeaponMorphAttackData[int.Parse(word[currentWordIndex++])];
            //basicAttack.staminaCost = float.Parse(word[currentWordIndex++]);
            //basicAttack.energyCost = float.Parse(word[currentWordIndex++]);
            //basicAttack.attackSpeed = float.Parse(word[currentWordIndex++]);
            //basicAttack.critChance = float.Parse(word[currentWordIndex++]);
            //basicAttack.fortitudeDamage = float.Parse(word[currentWordIndex++]);

            var basicAttack = new WeaponMorphAttackData();
            data.basicAttackData = new WeaponMorphAttackData[int.Parse(word[currentWordIndex++])];
            basicAttack.staminaCost = float.Parse(word[currentWordIndex++]);
            basicAttack.energyCost = float.Parse(word[currentWordIndex++]);
            basicAttack.attackSpeed = float.Parse(word[currentWordIndex++]);
            basicAttack.critChance = float.Parse(word[currentWordIndex++]);
            basicAttack.fortitudeDamage = float.Parse(word[currentWordIndex++]);

            for (int j = 0; j < data.basicAttackData.Length; j++)
            {
                data.basicAttackData[j] = basicAttack;
            }
            
            var heavyAttack = new WeaponMorphAttackData();
            data.heavyAttackData = new WeaponMorphAttackData[int.Parse(word[currentWordIndex++])];
            heavyAttack.staminaCost = float.Parse(word[currentWordIndex++]);
            heavyAttack.energyCost = float.Parse(word[currentWordIndex++]);
            heavyAttack.attackSpeed = float.Parse(word[currentWordIndex++]);
            heavyAttack.critChance = float.Parse(word[currentWordIndex++]);
            heavyAttack.fortitudeDamage = float.Parse(word[currentWordIndex++]);

            for (int j = 0; j < data.heavyAttackData.Length; j++)
            {
                data.heavyAttackData[j] = heavyAttack;
            }

            var SOName = data.morphName.Replace(" ", string.Empty);
            var finalOutputPath = outputDirectoryPath + "/" + SOName + "WeaponMorphData.asset";

            if (File.Exists(finalOutputPath))
            {
                AssetDatabase.DeleteAsset(finalOutputPath);
            }
            
            AssetDatabase.CreateAsset(data, finalOutputPath);
        }
    }
}
