using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Launcher emitter controller.
 */
public class LauncherEmitterController : MonoBehaviour
{
    /**
     * Visual Effect for shooting.
     * 
     * @param ParticleSystem
     */
    [SerializeField] protected ParticleSystem onShotVFX;

    /**
     * Life-time limit for the VFX ovject.
     * 
     * @param float
     */
    protected float vfxTimeToLive = 3.0f;

    /**
     * Starts a VFX creation coroutine.
     * 
     * @return void
     */
    public void EmitOnShotVFX()
    {
        if (onShotVFX != null)
        {
            StartCoroutine(OnShotVFXCoroutine());
        }        
    }

    /**
     * Creates a VFX.
     * 
     * @return IEnumerator
     */
    protected IEnumerator OnShotVFXCoroutine()
    {
        ParticleSystem generatedVFX = Instantiate(onShotVFX, transform.position, transform.rotation);
        yield return new WaitForSeconds(vfxTimeToLive);
        Destroy(generatedVFX.gameObject);
    }
}
