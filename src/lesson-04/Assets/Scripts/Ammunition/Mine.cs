using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Mine ammunition handler.
 */
public class Mine : ProjectileBase
{
    /**
     * Emulates mine explosion.
     * 
     * @param Collision
     *   An object collided with the mine.
     * 
     * @return void
     */
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider target in targets)
        {
            if (target.gameObject == GameManager.Instance.Player)
            {
                continue;
            }      

            if (target.gameObject.TryGetComponent(out Rigidbody targetRb))
            {
                targetRb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }
        Destroy(gameObject);
    }
}
