using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPlayer : SpawnPointBase
{
    protected override void Awake()
    {
        base.Awake();
        
        if (spawnablePrefab == null)
        {
            CharacterClassMetadata data = ClassesManager.Manager.PlayerClasses.GetRandomClass();
            spawnablePrefab = data.defaultPrefab;
            spawnablePrefab.AddBaseStats(data);
        }        
        
        Instantiate(spawnablePrefab.BaseStats.defaultPrefab, transform.position, Quaternion.identity);
        ArenaManager.Manager.SetPlayerObject(this);                
    }
}
