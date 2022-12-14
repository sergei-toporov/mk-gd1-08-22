using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public struct CharacterClassMetadata
{
    public string defaultName;
    public SpawnableBase defaultPrefab;
    public float baseHealth;
    public float baseMana;
    public float baseMovementSpeed;
    public float baseAttackSpeed;
    public float baseHealthRegeneration;
    public float baseManaRegeneration;
}

public class ClassesListBaseSO : ScriptableObject
{
    [SerializeField] protected GenericDictionary<string, CharacterClassMetadata> collection = new GenericDictionary<string, CharacterClassMetadata>();
    public GenericDictionary<string, CharacterClassMetadata> Collection { get => collection; }

    public CharacterClassMetadata GetRandomClass()
    {
        int collectionCount = collection.Count;
        if (collectionCount > 0)
        {
            string[] keys = new string[collectionCount];
            int counter = 0;
            foreach (string key in collection.Keys)
            {
                keys[counter] = key;
                counter++;
            }
            return collection[keys[UnityEngine.Random.Range(0, collectionCount)]];
        }

        throw new KeyNotFoundException($"The collection is empty");
    }

    public CharacterClassMetadata GetClassByKey(string key)
    {
        if (collection.TryGetValue(key, out CharacterClassMetadata characterClass))
        {
            return characterClass;
        }

        throw new KeyNotFoundException($"The '{key}' class is not in collection");
    }
}
