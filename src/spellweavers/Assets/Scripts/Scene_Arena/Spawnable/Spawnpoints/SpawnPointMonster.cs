using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointMonster : SpawnPointBase
{

    protected int maxSpawns = 5;
    protected int currentSpawns = 0;
    protected float spawnDelay = 4.0f;

    protected override void Awake()
    {
        base.Awake();
        if (spawnablePrefab == null)
        {
            CharacterClassMetadata data = ArenaResourceManager.Manager.MonsterClassesList.GetRandomClass();
            spawnablePrefab = data.defaultPrefab;
            spawnablePrefab.AddBaseStats(data);
        }
        //StartCoroutine(SpawnCoroutine());
    }

    protected IEnumerator SpawnCoroutine()
    {
        while (currentSpawns < maxSpawns)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(spawnablePrefab, transform.position, Quaternion.identity);
            currentSpawns++;
        }
    }
}
