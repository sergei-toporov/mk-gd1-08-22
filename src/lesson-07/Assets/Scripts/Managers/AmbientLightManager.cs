using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles light spawning.
 */
public class AmbientLightManager : MonoBehaviour
{
    /**
     * Spawn radius.
     * 
     * @param float.
     */
    [SerializeField] private float lightSpawnRadius = 5.0f;

    /**
     * Spawn time period.
     * 
     * @param Vector2.
     */
    [SerializeField] private Vector2 lightSpawnTimePeriod = new(15.0f, 45.0f);

    /**
     * Light object prefab.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject ambientLightPrefab;

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        StartCoroutine(RandomAmbientLightGenerationCoroutine());
    }

    /**
     * Handles light spawning.
     * 
     * @return IEnumerator.
     */
    private IEnumerator RandomAmbientLightGenerationCoroutine()
    {
        while (true)
        {
            float generationTime = Random.Range(lightSpawnTimePeriod.x, lightSpawnTimePeriod.y);
            yield return new WaitForSeconds(generationTime);

            Vector3 spawnPosition = GameManager.Manager.GetRandomPointNearPlayer(lightSpawnRadius);
            Instantiate(ambientLightPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
