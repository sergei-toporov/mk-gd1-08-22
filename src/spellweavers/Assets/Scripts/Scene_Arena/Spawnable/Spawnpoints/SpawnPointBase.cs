using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBase : MonoBehaviour
{
    [SerializeField] protected SpawnableBase spawnablePrefab;
    public SpawnableBase SpawnablePrefab { get => spawnablePrefab; }

    protected virtual void Awake()
    {
        if (!InitialCheck())
        {
            Debug.LogError("Errors occurred during initial checks. See logs.");
            Application.Quit();
        }
    }

    protected bool InitialCheck()
    {
        return true;
    }

}
