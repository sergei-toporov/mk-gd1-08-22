using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/**
 * Weapon switcher handler.
 */
public class WeaponSwitcherController : MonoBehaviour
{
    /**
     * Assigned cannon.
     * 
     * @param GameObject
     *   Cannon object.
     */
    [SerializeField] private GameObject cannon;

    /**
     * Assigned cannon's controller.
     * 
     * @param CannonController
     */
    private CannonController cannonController;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        cannonController = cannon.GetComponent<CannonController>();
    }
    /**
     * Triggers weapon switching.
     * 
     * @param Collider
     *   Collided object instance.
     *   
     * @return void
     */
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out PlayerController Player))
        {
            Player.SwitchCannon(cannonController);
        }
    }
}
