using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handler for the spawn manager.
 */
public class SpawnManager : MonoBehaviour
{
    /**
     * Manager instance.
     * 
     * @param SpawnManager
     */
    private static SpawnManager manager;
    public static SpawnManager Manager { get => manager; }

    /**
     * A set of spawnable objects to work with.
     * 
     * @param SpawnableObjectBase
     */
    [SerializeField] private SpawnableObjectsBase[] spawnablePrefabs;
    public SpawnableObjectsBase[] SpawnablePrefabs { get => spawnablePrefabs; }

    /**
     * Border value of spawnable area.
     * 
     * @param float
     */
    [SerializeField] float spawnableBorderValue = 8.5f;
    [SerializeField] float spawnableHeightValue = 0.7f;


    [SerializeField] private Vector2 objectRotationRange = new(0.0f, 360.0f);

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int prefabIndex = Random.Range(0, SpawnablePrefabs.Length);
            Quaternion rotation = Quaternion.Euler(0.0f, Random.Range(objectRotationRange.x, objectRotationRange.y), 0.0f);
            Instantiate(SpawnablePrefabs[prefabIndex], GetSpawnPoint(), rotation);
        }
    }

    /**
     * Generates a spawnpoint coords.
     * 
     * @return Vector3
     */
    private Vector3 GetSpawnPoint()
    {
        return new Vector3(
            Random.Range(-spawnableBorderValue, spawnableBorderValue),
            spawnableHeightValue,
            Random.Range(-spawnableBorderValue, spawnableBorderValue)
            );
    }
}
