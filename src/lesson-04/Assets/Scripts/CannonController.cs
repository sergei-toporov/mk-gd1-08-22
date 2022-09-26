using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cannon handler.
 */
public class CannonController : MonoBehaviour
{
    /**
     * Cannon controller component.
     * 
     * @param CannonController
     */
    private CannonController controller;
    public CannonController Controller {
        get
        {
            return controller = controller != null ? controller : GetComponent<CannonController>();
        }
    }

    /**
     * Assigned projectile.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject projectile;

    /**
     * Cannon reloading time
     * 
     * @param float
     */
    [SerializeField] private float reloadingTime = 2.0f;

    /**
     * Projectile emitter object.
     * 
     * @param ProjectileEmitterController
     */
    private ProjectileEmitterController emitter;

    /**
     * Cannon shot force.
     * 
     * @param float
     */
    [SerializeField][Range(0.0f, 10000.0f)] private float shotForce = 50;
    public float ShotForce { get => shotForce; }

    /**
     * Cannon's state flag.
     * 
     * @param bool
     */
    private bool isReloading = false;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    { 
        emitter = GetComponentInChildren<ProjectileEmitterController>();
        if (!projectile.TryGetComponent(out Rigidbody projectileRb))
        {
            Debug.LogError("The assigned ammunition type has no Rigidbody component: " + projectile.name);
        }
    }

    /**
     * Makes shot.
     * 
     * @return void
     */
    public void MakeShot()
    {
        if (!isReloading)
        {
            StartCoroutine(MakeShotCoroutine());
        }
    }

    /**
     * Processes the shot.
     * 
     * @return IEnumerator
     */
    private IEnumerator MakeShotCoroutine()
    {
        isReloading = true;
        var shell = Instantiate(projectile, emitter.transform.position, emitter.transform.rotation);
        var shellRb = shell.GetComponent<Rigidbody>();
        shellRb.AddForce(shell.transform.forward * shotForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
    }
}
