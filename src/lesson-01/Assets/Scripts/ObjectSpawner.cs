using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles game objects spawning.
 */
public class ObjectSpawner : MonoBehaviour
{
    /**
     * Set of game objects to spawn.
     * 
     * @param GameObject[]
     */
    [SerializeField] private GameObject[] prefabs;

    /**
     * An object responsible for spawning.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject spawner;

    /**
     * Spawn coords limits: minimum.
     * 
     * @param Vector3
     */
    [SerializeField] protected Vector3 spawnRangeMin = new Vector3(-10.0f, 0.5f, -10.0f);

    /**
     * Spawn coords limits: maximum.
     * 
     * @param Vector3
     */
    [SerializeField] protected Vector3 spawnRangeMax = new Vector3(10.0f, 5.0f, 10.0f);

    /**
     * Currently spawned object.
     * 
     * @param GameObject
     */
    private GameObject testSubject;

    /**
     * {@inheritdoc}
     */ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.Log("Error: no prefabs added into prefabs list");
                return;
            }

            if (spawner == null)
            {
                Debug.Log("Error: Spawner object is not assigned.");
            }

            if (testSubject != null)
            {
                GameObject.Destroy(testSubject, 1.0f);
            }

            var rotation = Quaternion.identity;
            var position = new Vector3(
                Random.Range(spawnRangeMin.x, spawnRangeMax.x),
                Random.Range(spawnRangeMin.y, spawnRangeMax.y),
                Random.Range(spawnRangeMin.z, spawnRangeMax.z)
                );
            testSubject = Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, rotation);
        }

        if (testSubject && testSubject.transform.position.y < -spawnRangeMin.y)
        {
            testSubject.transform.position = Vector3.zero;
        }
    }
}
