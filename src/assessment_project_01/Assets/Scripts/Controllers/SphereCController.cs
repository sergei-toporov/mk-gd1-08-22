using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCController : SpawnableObjectsBase
{

    private MeshFilter meshFilter;
    public MeshFilter MeshFilter
    {
        get
        {
            return meshFilter = meshFilter != null ? meshFilter : GetComponent<MeshFilter>();
        }
    }

    [SerializeField] private GameObject[] meshesPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void TriggerAction()
    {
        GameObject prefab = meshesPrefabs[Random.Range(0, meshesPrefabs.Length)];
        if (prefab.TryGetComponent(out MeshFilter targetMesh))
        {
            MeshFilter.mesh = targetMesh.sharedMesh;
        }

        /*
         * @todo add collider replacement
         */
        
    }
}
