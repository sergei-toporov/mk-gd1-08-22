using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_spawnable_monster_collection", menuName = "Custom Assets/Spawnable Monster Collection", order = 52)]
public class SpawnableMonstersCollection : ScriptableObject
{
    [SerializeField] protected List<SpawnableMonster> collection;
    public List<SpawnableMonster> Collection { get => collection; }


    public SpawnableMonster GetRandomPrefab()
    {
        return collection[Random.Range(0, collection.Count)];
    }
}
