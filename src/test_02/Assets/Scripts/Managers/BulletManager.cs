using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AmmoData
{
    public string Key;
    public GameObject StartVFX;
    public GameObject ExplosionVFX;
    public float LiveTime;
    public float ReactionTime;
    public int GunIndex;
}

public class BulletManager : MonoBehaviour
{
    protected static BulletManager manager;
    public static BulletManager Manager { get => manager; }

    [SerializeField] protected List<AmmoData> ammoTypes;
    public List<AmmoData> AmmoTypes { get => ammoTypes; }

    protected void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AmmoData GetAmmoByKey(string AmmoKey)
    {
        foreach (AmmoData ammoData in ammoTypes)
        {
            if (ammoData.Key == AmmoKey)
            {
                return ammoData;
            }
        }

        throw new KeyNotFoundException($"No such element in the ammo list: {AmmoKey}");
    }
}
