using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioController : MonoBehaviour
{

    private AudioClip clipToPlay;

    private AudioSource player;
    public AudioSource Player { get => player ?? GetComponent<AudioSource>(); }
    
    public void PlaySound(AudioClip clip)
    {
        clipToPlay = clip;
        StartCoroutine(PlaySoundCoroutine());
    }


    private IEnumerator PlaySoundCoroutine()
    {
        Player.PlayOneShot(clipToPlay);
        yield return new WaitForSeconds(clipToPlay.length);
        Destroy(gameObject);
    }
}
