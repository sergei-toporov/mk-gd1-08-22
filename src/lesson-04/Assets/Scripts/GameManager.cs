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
}
