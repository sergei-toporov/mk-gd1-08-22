using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A general projectile class.
 */
public class ProjectileBase : MonoBehaviour
{
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

    /**
     * Projectile's rigidbody component.
     * 
     * @param Rigidbody
     */
    protected Rigidbody projectileRb;
    public Rigidbody ProjectileRb { get => projectileRb; }

    /**
     * OnCollision visual effect.
     * 
     * @param ParticleSystem
     */
    [SerializeField] protected ParticleSystem collisionExplosionFX;
    public ParticleSystem CollisionExplosionFX { get => collisionExplosionFX; }

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
        Destroy(gameObject);
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
            if (collisionExplosionFX != null)
            {
                Instantiate(collisionExplosionFX, gameObject.transform.position, gameObject.transform.rotation);
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
}
