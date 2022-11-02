using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Projectile explosion controller. Used on VFX object.
 */
public class ProjectileExplosionController : MonoBehaviour
{
    /**
     * Time of life for the VFX.
     * 
     * @param float
     */
    [SerializeField] protected float timeToLive = 3.0f;

    /**
     * {@inheritDoc}
     */
    protected void Awake()
    {
        StartCoroutine(DestroyObjectCoroutine());
    }
    
    /**
     * Terminates the VFX object after its expiration.
     * 
     * @return IENumerator
     */
    protected IEnumerator DestroyObjectCoroutine()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
