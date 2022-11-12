using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/**
 * Projectile launcher controller.
 */
public class LauncherController_Old : MonoBehaviour
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
    public float DefaultReloadTime { get => defaultReloadTime; }

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
    public ProjectileBase AssignedProjectile { get => assignedProjectile; }

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
    public bool UseBurstFire { get => useBurstFire; }

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
    public float BurstFireDelay { get => burstFireDelay; }

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

    [SerializeField] protected string shotSFX;
    public string ShotSFX { get => shotSFX; }

    protected ObjectPool<ProjectileBase> pool;

    protected int baseDefaultCapacity = 100;
    protected int capacityMultiplier = 10;


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
        int defaultCapacity = 0;
        if (hasAssignedProjectile)
        {
            defaultCapacity = (int)(AssignedProjectile.TimeToLive / defaultReloadTime);
            defaultCapacity += defaultCapacity;
        }

        if (useBurstFire && maxProjectilesPerBurst > 0 && burstFireDelay > 0.0f)
        {
            burstFireDelay = defaultReloadTime / maxProjectilesPerBurst;
            reloadTime += burstFireDelay * maxProjectilesPerBurst;

            defaultCapacity *= (int)Mathf.Max(1, defaultReloadTime / burstFireDelay);
        }

        defaultCapacity = defaultCapacity > 0 ? defaultCapacity : baseDefaultCapacity;
        //pool = new ObjectPool<ProjectileBase>(AddObjectToPool, OnReleaseFromPool, OnReturnIntoPool, OnObjectDestroy, false, defaultCapacity, defaultCapacity * capacityMultiplier);
    }

    protected ProjectileBase AddObjectToPool()
    {
        ProjectileBase instance = Instantiate(AssignedProjectile, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectIntoPool;
        instance.gameObject.SetActive(false);
        return instance;
    }

    protected void ReturnObjectIntoPool(ProjectileBase instance)
    {
        pool.Release(instance);
    }

    protected void OnReleaseFromPool(ProjectileBase instance)
    {
        instance.gameObject.SetActive(true);

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
        StartCoroutine(EmitProjectileCoroutine());
    }

    protected IEnumerator EmitProjectileCoroutine()
    {
        emitter.EmitOnShotSFX();
        yield return new WaitForFixedUpdate();

        emitter.EmitOnShotVFX();
        yield return new WaitForFixedUpdate();

        GameObject projectile = Instantiate(assignedProjectile.gameObject, emitter.transform.position, emitter.transform.rotation);

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
