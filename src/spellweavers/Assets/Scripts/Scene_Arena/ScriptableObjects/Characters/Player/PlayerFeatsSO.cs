using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerFeat
{
    public string name;
    public AffectedStat affectedStat;
    public int maxLevel;
    public int currentLevel;
    public int improvementCostBase;
    public int currentImprovementCost;
    public int deltaPercent;
    public bool isActive;
}

[CreateAssetMenu(fileName = "new_player_feats_list", menuName = "Custom Assets/Feats List/Player Feats", order = 54)]
public class PlayerFeatsSO : ScriptableObject
{
    [SerializeField] protected GenericDictionary<string, PlayerFeat> collection = new GenericDictionary<string, PlayerFeat>();
    public GenericDictionary<string, PlayerFeat> Collection { get => collection; }

    public PlayerFeat GetFeatByKey(string key)
    {
        if (collection.TryGetValue(key, out PlayerFeat feat))
        {
            return feat;
        }

        throw new KeyNotFoundException($"The '{key}' feat is not in collection");
    }
}
