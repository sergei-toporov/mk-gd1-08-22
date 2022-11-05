using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Projectile launcher controller.
 */
public class LauncherController : MonoBehaviour
{
    /**
     * Launcher controller component.
     * 
     * @param LauncherController
     */
    [SerializeField] protected LauncherController controller;
    public LauncherController Controller { get => controller ?? GetComponent<LauncherController>(); }

    /**
     * Default reload time value.
     * 
     * @param float.
     */
    [SerializeField] protected float defaultReloadTime = 2.0f;

    /**
     * Force of the launcher's shot value.
     * 
     * @param float
     */
    [SerializeField] protected float shotForce = 100.0f;

    /**
     * Assigned ammunition type.
     * 
     * @param ProjectileBase
     */
    [SerializeField] protected ProjectileBase assignedProjectile;

    /**
     * Does launcher have assigned ammunition type?
     * 
     * @param bool
     */
    protected bool hasAssignedProjectile = false;

    /**
     * If launcher shoots by burst or by single projectile.
     * 
     * @param bool
     */
    [SerializeField] protected bool useBurstFire = false;

    /**
     * Number of shots per burst.
     * 
     * @param int
     */
    [SerializeField] protected int maxProjectilesPerBurst = 0;

    /**
     * Delay between burst shots. Affects total reload time.
     * 
     * @param float
     */
    [SerializeField] protected float burstFireDelay = 0.1f;

    /**
     * Burst shots counter to track shooting cycles.
     * 
     * @param int
     */
    protected int currentBurstShotProjectiles = 0;
    
    /**
     * An emitter for projectiles.
     * 
     * @param LauncherEmitterController.
     */
    protected LauncherEmitterController emitter;

    /**
     * Whether launsher is reloading or not.
     * 
     * @param bool
     */
    [SerializeField] protected bool isReloading = false;

    /**
     * Total reload time: depends on the burst dire mode.
     * 
     * @param float
     */
    protected float reloadTime;

    /**
     * {@inheritDoc}
     */
    protected void Awake()
    {
        reloadTime = defaultReloadTime;
        hasAssignedProjectile = assignedProjectile != null;
        emitter = GetComponentInChildren<LauncherEmitterController>();
        if (emitter == null)
        {
            Debug.LogError("No 'LauncherEmitterController' component on the emitter child.");
        }

        if (useBurstFire && maxProjectilesPerBurst > 0 && burstFireDelay > 0.0f)
        {
            burstFireDelay = defaultReloadTime / maxProjectilesPerBurst;
            reloadTime += burstFireDelay * maxProjectilesPerBurst;
        }
    }

    /**
     * Makes shot.
     * 
     * @return void
     */
    public void Shoot()
    {
        if (!isReloading)
        {
            StartCoroutine(ShootCoroutine());
        }
        
    }

    /**
     * Shooting coroutine.
     * 
     * @return IEnumerator
     */
    protected IEnumerator ShootCoroutine()
    {
        isReloading = true;
        if (hasAssignedProjectile)
        {
            StartShooting();
        }
        else
        {
            Debug.LogError("Pew-pew! No ammo assigned!");
        }

        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    /**
     * Firestarter.
     * 
     * @return void
     */
    protected void StartShooting()
    {
        if (useBurstFire)
        {
            StartCoroutine(BurstFireCoroutine());
        }
        else
        {
            EmitProjectile();
        }
    }

    /**
     * Burst fire coroutine.
     * 
     * @return IEnumerator
     */
    protected IEnumerator BurstFireCoroutine()
    {
        while (currentBurstShotProjectiles++ < maxProjectilesPerBurst)
        {
            EmitProjectile();
            yield return new WaitForSeconds(burstFireDelay);
        }
        currentBurstShotProjectiles = currentBurstShotProjectiles >= maxProjectilesPerBurst ? 0 : currentBurstShotProjectiles;
        
    }

    /**
     * Pushing a projectile out of the emitter.
     * 
     * @return void
     */
    protected virtual void EmitProjectile()
    {
        GameObject projectile = Instantiate(assignedProjectile.gameObject, emitter.transform.position, emitter.transform.rotation);
        emitter.EmitOnShotVFX();
        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.AddForce(emitter.transform.forward * shotForce, ForceMode.Impulse);
        }
        else
        {
            // Here can be logic to move a projectile without rigidbody. Just destroying for now.
            Destroy(projectile);
        }
    }
}
