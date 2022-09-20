using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weapon switcher handler.
 */
public class WeaponSwitcherController : MonoBehaviour
{
    /**
     * Assigned cannon type.
     * 
     * @param int
     *   Cannon type index.
     */
    [SerializeField] private int cannonType;
    public int CannonType { get => cannonType; }

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
        if (collider.name == "RobotCollider")
        {
            GameManager.Instance.Player.GetComponent<PlayerController>().SwitchCannon(gameObject);
        }
    }
}
