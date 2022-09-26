using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A general projectile handler.
 */
public class ProjectileBase : MonoBehaviour
{
    /**
     * Mine blast radius.
     * 
     * @param float
     */
    [SerializeField][Range(0.0f, 10.0f)] protected float blastRadius = 30.0f;

    /**
     * Mine blast force.
     * 
     * @param float
     */
    [SerializeField][Range(0.0f, 50000.0f)] protected float blastForce = 40000.0f;

    /**
     * Amount of time till the projectile's self-destruction.
     * 
     * @param float.
     */
    [SerializeField] protected float timeToLive = 10.0f;
    
    /**
     * Projectile's rigidbody.
     * 
     * @param Rigidbody
     */
    protected Rigidbody projectileRb;

    /**
     * {@inheritdoc}
     */
    protected virtual void Awake()
    {
        projectileRb = gameObject.GetComponent<Rigidbody>();
    }

    /**
     * {@inheritdoc}
     */
    protected virtual void Start()
    {
        StartCoroutine(DestroyOnExpiration());
    }
    
    /**
     * Keeps a projectile alive for some time.
     * 
     * @return IEnumerator.
     */
    protected IEnumerator DestroyOnExpiration()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

}
