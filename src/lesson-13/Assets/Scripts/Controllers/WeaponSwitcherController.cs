using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Weapon switcher controller.
 */
public class WeaponSwitcherController : MonoBehaviour
{
    /**
     * Assigned launcher.
     * 
     * @param LauncherController
     */
    [SerializeField] protected LauncherController assignedLauncher;

    /**
     * If switcher has assigned launcher.
     * 
     * @param bool
     */
    protected bool hasLauncherAssigned = false;

    /**
     * {@inheritDoc}
     */
    protected void Awake()
    {
        hasLauncherAssigned = assignedLauncher != null;
    }

    /**
     * Triggers launcher switching on collider triggering.
     * 
     * @param Collider
     * 
     * @return void
     */
    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController Player))
        {
            if (!hasLauncherAssigned)
            {
                Debug.LogError("No launcher assigned to this pad: " + gameObject.name);
                return;
            }

            Player.TriggerWeaponSwitch(assignedLauncher);
        }
    }
}
