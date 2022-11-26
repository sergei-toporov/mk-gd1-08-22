using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSwitcherScript : MonoBehaviour
{
    public AmmoBase bullet;
    
    void Start() {  }

    void Update() { }

    /**
     * Changed to OnTriggerEnter for one time triggering.
     * 
     * Changed direct affecting on player with triggering character's method.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController Player))
        {
            Player.SwitchActiveGun(bullet);
        }
    }
}
