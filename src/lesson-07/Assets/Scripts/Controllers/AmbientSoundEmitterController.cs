using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ambient sounds handler for the randomly spawned sound.
 */
public class AmbientSoundEmitterController : MonoBehaviour
{
    /**
     * Audio source component.
     * 
     * @param AudioSource
     */
    protected AudioSource ambientPlayer;
    public AudioSource AmbientPlayer
    {
        get
        {
            return ambientPlayer = ambientPlayer != null ? ambientPlayer : GetComponent<AudioSource>();
        }
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        AmbientPlayer.PlayOneShot(AmbientAudioManager.Manager.AmbientSounds[Random.Range(0, AmbientAudioManager.Manager.AmbientSounds.Length)]);
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (!AmbientPlayer.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
