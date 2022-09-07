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
     * An object which is responsible for spawning.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject spawner;

    /**
     * X-axis limits for spawning.
     * 
     * @param List<float>
     */
    [SerializeField] private List<float> xRange = new List<float> { -10.0f, 10.0f };

    /**
     * Y-axis limits for spawning.
     * 
     * @param List<float>
     */
    [SerializeField] private List<float> yRange = new List<float> { 1.0f, 10.0f };

    /**
     * Z-axis limits for spawning.
     * 
     * @param List<float>
     */
    [SerializeField] private List<float> zRange = new List<float> { -10.0f, 10.0f };

    /**
     * Currently spawned object.
     * 
     * @param GameObject
     */
    private GameObject testSubject;

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
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.Log("Error: no prefabs added into prefabs list");
                return;
            }

            if (testSubject != null)
            {
                GameObject.Destroy(testSubject, 1.0f);
            }

            var rotation = Quaternion.identity;
            var position = new Vector3(
                Random.Range(xRange[0], xRange[1]),
                Random.Range(yRange[0], yRange[1]),
                Random.Range(zRange[0], zRange[1])
                );
            testSubject = Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, rotation);
        }
    }
}
