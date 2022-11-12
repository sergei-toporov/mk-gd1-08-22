using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LauncherController : MonoBehaviour
{
    /// <summary>
    ///     An instance of controller.
    /// </summary>
    [SerializeField] protected LauncherController instance;
    public LauncherController Instance { get => instance ?? GetComponent<LauncherController>(); }

    /// <summary>
    ///     An instance of emitter.
    /// </summary>
    [SerializeField] protected LauncherEmitterController emitter;
    public LauncherEmitterController Emitter { get => emitter; }

    /// <summary>
    ///     An object to keep pool items.
    /// </summary>
    protected PoolStoreController poolStore;
    public PoolStoreController PoolStore { get => poolStore; }

    /// <summary>
    ///     A default reload time. Used as base.
    /// </summary>
    [SerializeField] protected float defaultReloadTime = 2.0f;
    public float DefaultReloadTime { get => defaultReloadTime; }

    /// <summary>
    ///     Launcher shot force. Bigger - stronger.
    /// </summary>
    [SerializeField] protected float shotForce = 100.0f;
    public float ShotForce { get => shotForce; }

    /// <summary>
    ///     An assigned projectile type to shoot.
    /// </summary>
    [SerializeField] protected ProjectileBase assignedProjectile;
    public ProjectileBase AssignedProjectile { get => assignedProjectile; }

    /// <summary>
    ///     Bursts firing mode flag.
    /// </summary>
    [SerializeField] protected bool useBurstFire = false;
    public bool UseBurstFire { get => useBurstFire; }

    /// <summary>
    ///     A delay between single shots in the burst firing mode.
    /// </summary>
    [SerializeField] protected float burstFireDelay = 0.1f;
    public float BurstFireDelay { get => burstFireDelay; }

    /// <summary>
    ///     Amount of shots per burst.
    /// </summary>
    [SerializeField] protected int projectilesPerBurst = 1;
    public int ProjectilesPerBurst { get => projectilesPerBurst; }

    /// <summary>
    ///     Current amount of shots made in burst.
    /// </summary>
    protected int currentBurstShots = 0;

    /// <summary>
    ///     Reloading state flag.
    /// </summary>
    [SerializeField] protected bool isReloading = false;

    /// <summary>
    ///     Actual reload time.
    /// </summary>
    protected float reloadTime;

    /// <summary>
    ///     Ammo pool object.
    /// </summary>
    protected ObjectPool<ProjectileBase> ammoPool;
    public ObjectPool<ProjectileBase> AmmoPool { get => ammoPool; }

    /// <summary>
    ///     Pool capacity multiplier for setting of the maximum pool capacity.
    /// </summary>
    [SerializeField] protected int poolCapacityMultiplier = 10;

    /// <summary>
    ///     Ammo pooling state flag.
    /// </summary>
    [SerializeField] protected bool useAmmoPool = true;
    public bool UseAmmoPool { get => useAmmoPool; }

    /// <summary>
    ///     Pool pre-warming state.
    ///     
    ///     If TRUE, pool objects will be created whithin initial configuration.
    ///     If FALSE, pool will be filled shot by shot.
    /// </summary>
    [SerializeField] protected bool prewarmAmmoPool = true;


    protected void Awake()
    {
        if (!InitialConfiguration())
        {
            Debug.LogError("Initial configuration error. Can't proceed!");
            Application.Quit();
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

    /// <summary>
    ///     A launcher initial configuration.
    /// </summary>
    /// <returns>
    ///     Returns:
    ///     TRUE - configuration completed successfully.
    ///     FALSE - configuration error occurred.
    /// </returns>
    protected bool InitialConfiguration()
    {
        if (assignedProjectile == null)
        {
            Debug.LogError("No assigned projectile.");
            return false;
        }

        emitter = GetComponentInChildren<LauncherEmitterController>();
        if (emitter == null)
        {
            Debug.LogError("No 'LauncherEmiiterController' assigned to any of children");
            return false;
        }

        reloadTime = defaultReloadTime;
        if (useBurstFire)
        {
            if (projectilesPerBurst < 1)
            {
                Debug.LogError("The 'Max Projectiles Pre Burst' value cannot be lesser than 1.");
                return false;
            }

            if (burstFireDelay <= 0.0f)
            {
                Debug.LogError("The 'Burst Fire Delay' value should be greater than 0.0f.");
                return false;
            }

            reloadTime += burstFireDelay * projectilesPerBurst;
        }

        if (useAmmoPool)
        {
            if (poolCapacityMultiplier <= 0)
            {
                Debug.LogError("The 'Pool Capacity Multiplier' value should be greater than 0.");
                return false;
            }

            GameObject poolStoreObject = new GameObject(gameObject.name + "_PoolStore");
            poolStore = poolStoreObject.AddComponent<PoolStoreController>();

            int poolCapacity = (int)(AssignedProjectile.TimeToLive / defaultReloadTime);
            poolCapacity += poolCapacity;

            if (useBurstFire)
            {
                poolCapacity *= (int)Mathf.Max(1, defaultReloadTime / burstFireDelay); 
            }
            ammoPool = new ObjectPool<ProjectileBase>(AddToPool, OnGetFromPool, OnReleaseIntoPool, OnObjectDestroy, false, poolCapacity, poolCapacity * poolCapacityMultiplier);

            if (prewarmAmmoPool)
            {
                ProjectileBase[] preWarmPool = new ProjectileBase[poolCapacity];
                for (int i = 0; i < poolCapacity; i++)
                {
                    preWarmPool[i] = ammoPool.Get();
                }

                for (int i = 0; i < poolCapacity; i++)
                {
                    ammoPool.Release(preWarmPool[i]);
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Adds projectile to ammo pool.
    /// </summary>
    /// <returns>
    ///     An instance of "ProjectileBase" or its child.
    /// </returns>
    protected ProjectileBase AddToPool()
    {
        ProjectileBase projectile = Instantiate(assignedProjectile, Vector3.zero, Quaternion.identity);
        projectile.Disable += ReleaseIntoPool;
        projectile.gameObject.SetActive(false);
        projectile.SetLauncher(this);
        projectile.transform.parent = poolStore.transform;
        return projectile;
    }

    /// <summary>
    ///     Triggers on getting a projectile from ammo pool. Makes it active.
    /// </summary>
    /// <param name="projectile">
    ///     An instance of "ProjectileBase" or its child.
    /// </param>
    protected void OnGetFromPool(ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(true);
        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.isKinematic = false;
        }
        projectile.transform.position = emitter.transform.position;
        projectile.transform.rotation = emitter.transform.rotation;
    }

    /// <summary>
    ///     Triggers on returning projectile into ammo pool. Makes it inactive.
    /// </summary>
    /// <param name="projectile">
    ///     An instance of "ProjectileBase" or its child.    
    /// </param>
    protected void OnReleaseIntoPool(ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(false);
        if (projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            projectileRb.velocity = Vector3.zero;
            projectileRb.angularVelocity = Vector3.zero;
            projectileRb.isKinematic = true;
        }

        if (projectile.TryGetComponent(out TrailRenderer projectileTR))
        {
            projectileTR.Clear();
        }
    }

    /// <summary>
    ///     Triggers on projectile destroy.
    /// </summary>
    /// <param name="projectile">
    ///     An instance of "ProjectileBase" or its child.    
    /// </param>
    protected void OnObjectDestroy(ProjectileBase projectile)
    {
        Destroy(projectile.gameObject);
    }

    /// <summary>
    ///     Returns projectile into ammo pool.
    /// </summary>
    /// <param name="projectile">
    ///     An instance of "ProjectileBase" or its child.
    /// </param>
    protected void ReleaseIntoPool(ProjectileBase projectile)
    {
        ammoPool.Release(projectile);
    }

    public void Shoot()
    {
        if (!isReloading)
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    protected IEnumerator ShootCoroutine()
    {
        isReloading = true;
        if (useBurstFire)
        {
            StartCoroutine(BurstFireCoroutine());
        }
        else
        {
            EmitProjectile();
        }

        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    protected IEnumerator BurstFireCoroutine()
    {
        while (currentBurstShots++ < projectilesPerBurst)
        {
            EmitProjectile();
            yield return new WaitForSeconds(burstFireDelay);
        }
        currentBurstShots = currentBurstShots >= projectilesPerBurst ? 0 : currentBurstShots;
    }

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

        ProjectileBase projectile = useAmmoPool
            ? ammoPool.Get()
            : Instantiate(assignedProjectile, emitter.transform.position, emitter.transform.rotation);

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
