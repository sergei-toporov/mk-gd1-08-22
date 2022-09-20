using System.Collections;
using System.Collections.Generic;
using TSN_Utility;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Game manager handler.
 */
public class GameManager : MonoBehaviour
{
    /**
     * A collection of projectile types.
     *
     * @param GameObject[]
     */
    [SerializeField] private GameObject[] ammunition;

    /**
     * Main character game object.
     * 
     * @param GameObject
     */
    private GameObject player;
    public GameObject Player { get => player; }

    /**
     * Instance of this manager to use across the project's scripts.
     * 
     * @param GameManager
     */
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }        
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject targetsCollection = GameObject.Find("Targets");

        foreach (Transform target in targetsCollection.transform)
        {
            target.AddComponent<TargetController>();
        }

    }

    /**
     * Returns ammunition type related to the provided cannon type.
     * 
     * @param int cannonType
     *   Index of the cannon type.
     *   
     * @return GameObject
     *   Ammunition type game object.
     */
    public GameObject GetAmmoByCannonType (int cannonType)
    {
        int ammoType = cannonType - 1;

        if (ammoType < 0 || ammoType >= ammunition.Length)
        {
            return new GameObject("DumpAmmo");
        }

        return ammunition[ammoType];
    }
}
