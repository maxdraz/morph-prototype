using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;

public class WeaponMorphCSVParser : CSVParser
{

    public WeaponMorphCSVParser()
    {
        CSVPath = Application.dataPath + "/Editor/CSV/WeaponMorphs.csv";
        outputDirectoryPath = "Assets/Scripts/Morphs/WeaponMorphs/Data";
        
        TryReadFile(CSVPath);
    }

    public override void Parse()
    {
        if (!FileReadSuccessfully()) return;
        
        //ClearDirectoryContents(outputDirectoryPath);

        //skip first line as its just headers
        for (int i = 1; i < allLines.Length; i++)
        {
            // get words in line
            var words = allLines[i].Split(',');
            int currentWordIndex = 0;
            //create SO
            var data = ScriptableObject.CreateInstance<WeaponMorphData>();
            //morph name
            data.morphName = words[currentWordIndex++];
            
            //creature types
            List<string> creatureTypes = new List<string>();

            if (words[currentWordIndex].StartsWith(("\"")))
            {
                var shouldLoop = true;
                while (shouldLoop)
                {
                    var currentCreature = words[currentWordIndex].Trim('"', ' ');

                    creatureTypes.Add(currentCreature);
                    shouldLoop = !words[currentWordIndex].EndsWith("\"");
                    currentWordIndex++;
                }
            }
            else
            {
                creatureTypes.Add(words[currentWordIndex++]);
                
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
                            break;
                        } else
                        {
                            // creature type doesn't exist, error
                            Debug.LogError("Build Aborted : Creature type doesn't exist \nexcel location: row = " + (i +1) + " word = " + creatureTypes[x]);
                            return;
                        }
                    }
                   
                }
            }

            // base damage
            data.baseDamage = float.Parse(words[currentWordIndex++]);
            
            // attack data
            var basicAttack = new WeaponMorphAttackData();
            data.basicAttackData = new WeaponMorphAttackData[int.Parse(words[currentWordIndex++])];
            basicAttack.staminaCost = float.Parse(words[currentWordIndex++]);
            basicAttack.energyCost = float.Parse(words[currentWordIndex++]);
            basicAttack.attackSpeed = float.Parse(words[currentWordIndex++]);
            basicAttack.critChance = float.Parse(words[currentWordIndex++]);
            basicAttack.fortitudeDamage = float.Parse(words[currentWordIndex++]);

            for (int j = 0; j < data.basicAttackData.Length; j++)
            {
                data.basicAttackData[j] = basicAttack;
            }
            
            var heavyAttack = new WeaponMorphAttackData();
            data.heavyAttackData = new WeaponMorphAttackData[int.Parse(words[currentWordIndex++])];
            heavyAttack.staminaCost = float.Parse(words[currentWordIndex++]);
            heavyAttack.energyCost = float.Parse(words[currentWordIndex++]);
            heavyAttack.attackSpeed = float.Parse(words[currentWordIndex++]);
            heavyAttack.critChance = float.Parse(words[currentWordIndex++]);
            heavyAttack.fortitudeDamage = float.Parse(words[currentWordIndex++]);

            for (int j = 0; j < data.heavyAttackData.Length; j++)
            {
                data.heavyAttackData[j] = heavyAttack;
            }
            
            //final 
            var SOName = data.morphName.Replace(" ", string.Empty);
            var finalOutputPath = outputDirectoryPath + "/" + SOName + "WeaponMorphData.asset";
            
            UpdateAssetDatabase(data, in finalOutputPath);
        }
        
        //success
        Debug.Log("**Success** rebuilding weapon morph databse");
    }
}
