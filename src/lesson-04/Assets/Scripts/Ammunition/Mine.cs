using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Mine ammunition handler.
 */
public class Mine : MonoBehaviour
{
    /**
     * Projectile's rigidbody.
     * 
     * @param Rigidbody
     */
    private Rigidbody projectileRb;

    /**
     * Time until object will be destroyed after instantiating.
     *
     * @param float
     */
    private readonly float timeTillDestroy = 10.0f;

    /**
     * Mine blast radius.
     * 
     * @param float
     */
    private float blastRadius = 30.0f;

    /**
     * Mine blast force.
     * 
     * @param float
     */
    private float blastForce = 400000.0f;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        projectileRb = gameObject.GetComponent<Rigidbody>();
    }
    
    /**
     * {@inheritdoc}
     */
    void Start()
    {
        var shotForce = GameManager.Instance.Player.GetComponent<PlayerController>()
            .ActiveCannon.GetComponent<CannonController>().ShotForce;

        projectileRb.AddForce(this.transform.forward * shotForce, ForceMode.Impulse);
        StartCoroutine(DestroyOnExpiration());
    }

    /**
     * Destroys object on its expiration.
     * 
     * @return IEnumerator
     */
    private IEnumerator DestroyOnExpiration()
    {
        yield return new WaitForSeconds(timeTillDestroy);
        Destroy(gameObject);
    }

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
            Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                targetRigidbody.AddExplosionForce(blastForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
