using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player's sound and music handler.
 */
public class PlayerAudioController : MonoBehaviour
{
    /**
     * Music playlist.
     * 
     * @param AudioClip[]
     */
    [SerializeField] private AudioClip[] playlist;
    public AudioClip[] Playlist { get => playlist; }

    /**
     * Footsteps sound.
     * 
     * @param AudioClip
     */
    [SerializeField] private AudioClip footsteps;

    /**
     * Audio Source component.
     * 
     * @param AudioSource
     */
    private AudioSource musicPlayer;
    public AudioSource MusicPlayer
    {
        get
        {
            return musicPlayer = musicPlayer != null ? musicPlayer : GetComponent<AudioSource>();
        }
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (!MusicPlayer.isPlaying)
        {
            MusicPlayer.PlayOneShot(playlist[Random.Range(0, playlist.Length)]);
        }
    }

    /**
     * Plays footstep sound.
     * 
     * @return void
     */
    public void PlayFootstep()
    {
        MusicPlayer.PlayOneShot(footsteps);
    }
}
