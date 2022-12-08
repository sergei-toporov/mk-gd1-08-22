using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBase : MonoBehaviour
{
    [SerializeField] protected SpawnableBase spawnablePrefab;
    public SpawnableBase SpawnablePrefab { get => spawnablePrefab; }



}
