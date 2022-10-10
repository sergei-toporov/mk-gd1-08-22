using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A base class for the spawners.
 */
public class SpawnerControllerBase : MonoBehaviour
{
    /**
     * A prefab to spawn.
     */
    [SerializeField] protected GameObject objectPrefab;
    public GameObject ObjectPrefab { get => objectPrefab; }
}
