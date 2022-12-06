using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPlayer : SpawnPointBase
{
    protected void Awake()
    {
        Instantiate(spawnablePrefab, transform.position, Quaternion.identity);
        ArenaManager.Manager.SetPlayerObject(this);
    }
}
