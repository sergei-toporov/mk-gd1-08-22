using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cannon controller.
 */
public class CannonController : MonoBehaviour
{
    /**
     * Cannon index.
     * 
     * @param int
     */
    [SerializeField] private int cannonType = 0;
    public int CannonType { get => cannonType; }

    /**
     * Cannon's ammunition.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject projectile;
    public GameObject Projectile { get => projectile; }

    /**
     * Cannon reloading time
     * 
     * @param float
     */
    [SerializeField] private float reloadingTime = 2.0f;

    /**
     * Projectile emitter object.
     * 
     * @param ProjectileEmitterController
     */
    private ProjectileEmitterController emitter;

    /**
     * Cannon shot force.
     * 
     * @param float
     */
    [SerializeField][Range(0.0f, 10000.0f)] private float shotForce = 50;
    public float ShotForce { get => shotForce; }

    /**
     * Cannon's state flag.
     * 
     * @param bool
     */
    private bool isReloading = false;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        projectile = GameManager.Instance.GetAmmoByCannonType(cannonType);
        emitter = this.GetComponentInChildren<ProjectileEmitterController>();
    }

    /**
     * Shoots if it's not in reloading mode.
     * 
     * @return void
     */
    public void MakeShot()
    {
        if (!isReloading)
        {
            StartCoroutine(ProcessShot());
        }
    }

    /**
     * Shooting process handler.
     * 
     * @return IEnumerator
     */
    private IEnumerator ProcessShot()
    {
        isReloading = true;
        Instantiate(projectile, emitter.transform.position, emitter.transform.rotation);
        yield return new WaitForSeconds(reloadingTime);

        isReloading = false;
    }
}
