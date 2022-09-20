using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cannon projectile handler.
 */
public class CannonProjectile : MonoBehaviour
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

        projectileRb.AddForce(this.transform.up * shotForce, ForceMode.Impulse);
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
}
