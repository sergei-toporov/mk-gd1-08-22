using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A general projectile class.
 */
public class ProjectileBase : MonoBehaviour
{
    protected ProjectileBase instance;
    public ProjectileBase Instance { get => instance ?? GetComponent<ProjectileBase>(); }

    /**
     * Whether explodes or not.
     * 
     * @param bool
     */
    [SerializeField] protected bool isExplodable = false;

    /**
     * Terminates on contact or not.
     * 
     * @param bool
     */
    [SerializeField] protected bool destroyOnCollision = false;

    /**
     * Explosion radius value.
     * 
     * @param float.
     */
    [SerializeField] protected float explosionRadius = 0.0f;

    /**
     * Explosion force value.
     * 
     * @param float
     */
    [SerializeField] protected float explosionForce = 0.0f;

    /**
     * Projectiles lifetime.
     * 
     * @param float.
     */
    [SerializeField] protected float timeToLive = 10.0f;
    public float TimeToLive { get => timeToLive; }

    /**
     * Projectile's rigidbody component.
     * 
     * @param Rigidbody
     */
    protected Rigidbody projectileRb;
    public Rigidbody ProjectileRb { get => projectileRb; }

    /**
     * Collision visual effect.
     * 
     * @param ParticleSystem
     */
    [SerializeField] protected ParticleSystem collisionVFX;

    /**
     * Explosion visual effect.
     * 
     * @param ParticleSystem
     */
    [SerializeField] protected ParticleSystem explosionVFX;

    [SerializeField] protected bool playOnCollideSFX = false;
    public bool PlayOnCollideSFX { get => playOnCollideSFX; }

    [SerializeField] protected string onCollideSFX;
    public string OnCollideSFX { get => OnCollideSFX; }

    [SerializeField] protected bool playOnDestroySFX = false;
    public bool PlayOnDestroySFX { get => playOnDestroySFX; }

    [SerializeField] protected string onDestroySFX;
    public string OnDestroySFX { get => onDestroySFX; }

    public delegate void OnDisableCallback(ProjectileBase Instance);

    public OnDisableCallback Disable;

    [SerializeField] protected bool isPooled = false;
    protected LauncherController launcher;


    /**
     * {@inheritDoc}
     */
    protected virtual void Awake()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            projectileRb = rb;
        }
        else
        {
            Debug.LogError("The '" + gameObject.name + "' prefab has no Rigidbody component. Please, fix that!");
        }
    }

    /**
     * {@inheritDoc}
     */
    protected virtual void Start()
    {
        
    }

    protected void OnEnable()
    {
        StartCoroutine(ProjectileDestroyCoroutine());
    }

    /**
     * Calls the projectile termination on its expiration.
     * 
     * @param IEnumerator.
     */
    protected virtual IEnumerator ProjectileDestroyCoroutine()
    {
        yield return new WaitForSeconds(timeToLive);
        DestroyProjectile();
    }

    /**
     * Destroys projectile.
     * 
     * @return void
     */
    protected virtual void DestroyProjectile()
    {
        if (playOnDestroySFX && onDestroySFX != null)
        {
            AudioManager.Manager.PlaySfxByKeyInGameObjectPosition(onDestroySFX, gameObject);
        }
        if (isPooled)
        {
            Disable(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    /**
     * Defines projectile's behaviour on collision.
     * 
     * @return void
     */
    protected void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<PlayerController>() == null && collision.gameObject.GetComponent<ProjectileBase>() == null)
        {
            if (playOnCollideSFX && onCollideSFX != null)
            {
                AudioManager.Manager.PlaySfxByKeyInGameObjectPosition(onCollideSFX, gameObject);
            }
            
            if (collisionVFX != null)
            {
                Instantiate(collisionVFX, gameObject.transform.position, gameObject.transform.rotation);
            }

            if (isExplodable)
            {
                ExplodeProjectile();
            }


            if (destroyOnCollision)
            {
                DestroyProjectile();
            }            
        }        
    }

    /**
     * Explodes projectile.
     * 
     * @param void
     */
    protected void ExplodeProjectile()
    {
        Instantiate(explosionVFX, gameObject.transform.position, gameObject.transform.rotation);
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius);        

        foreach (Collider target in targets)
        {
            if (target.gameObject == GameManager.Manager.Player)
            {
                continue;
            }

            if (target.gameObject.TryGetComponent(out Rigidbody targetRb))
            {
                targetRb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    public void SetLauncher(LauncherController providedLauncher)
    {
        launcher = providedLauncher;
        isPooled = launcher.UseAmmoPool;        
    }
}
