using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] protected int gun = 0;
    public int Gun { get => gun; }
    [SerializeField] protected bool isCollided = false;
    [SerializeField] protected string ammoKey;

    void Start()
    {
        if (ammoKey == null)
        {
            Debug.LogError("Ammo Key value is not set");
        }

        var ammoData = BulletManager.Manager.GetAmmoByKey(ammoKey);
        //var data = Data.Single(s => gameObject.name.Contains(s.Key));
        var effectInstance = Instantiate(ammoData.StartVFX, transform.position, Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = 3.0f;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }

    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollided == true)
        {
            return;
        }
        isCollided = true;
        var ammoData = BulletManager.Manager.GetAmmoByKey(ammoKey);
        //var data = Data.Single(s => gameObject.name.Contains(s.Key));

        var timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = ammoData.ReactionTime;
        timer.OnTime = () =>
        {
            //var data = Data.Single(s => gameObject.name.Contains(s.Key));
            var ammoData = BulletManager.Manager.GetAmmoByKey(ammoKey);
            var effectInstance = Instantiate(ammoData.ExplosionVFX, collision.GetContact(0).point, Quaternion.identity);
            effectInstance.transform.rotation *= Quaternion.FromToRotation(effectInstance.transform.up, collision.GetContact(0).normal);
            Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        };

        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = ammoData.LiveTime;
        timer.OnTime = () =>
        {
            Destroy(gameObject);
        };
    }

}
