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

    /// <summary>
    ///     A key of sound effect playable on shot.
    /// </summary>
    [SerializeField] protected string onShotSFXKey;

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

    public void EmitOnShotSFX()
    {
        if (onShotSFXKey != null)
        {
            AudioManager.Manager.PlaySfxByKeyInGameObjectPosition(onShotSFXKey, gameObject);
        }
        else
        {
            Debug.LogError("Unable to get the 'LauncherController' component from the parent or the 'onShotSFXKey' field is empty");
        }
    }
}
