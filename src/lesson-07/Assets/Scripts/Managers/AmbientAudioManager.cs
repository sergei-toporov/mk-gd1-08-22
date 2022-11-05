using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles ambient sounds spawning.
 */
public class AmbientAudioManager : MonoBehaviour
{
    /**
     * Ambient audio manager component.
     * 
     * @param AmbientAudioManager
     */
    private static AmbientAudioManager manager;
    public static AmbientAudioManager Manager { get => manager; }

    /**
     * Ambient sounds collection for spawning.
     * 
     * @param AudioClip[]
     */
    [SerializeField] private AudioClip[] ambientSounds;
    public AudioClip[] AmbientSounds { get => ambientSounds; }

    /**
     * Spawn time period.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 ambientSoundSpawnTimePeriod = new(20.0f, 60.0f);

    /**
     * Spawn radius.
     * 
     * @param float.
     */
    [SerializeField] protected float ambientSoundSpawnRadius = 15.0f;

    /**
     * Sound emitter prefab object.
     * 
     * @param GameObject
     */
    [SerializeField] GameObject ambientSoundEmitterPrefab;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        manager = manager != this ? this : manager;
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        StartCoroutine(RandomAmbientSoundGenerationCoroutine());
    }

    /**
     * Handles sound generation.
     * 
     * @return IEnumerator.
     */
    private IEnumerator RandomAmbientSoundGenerationCoroutine()
    {
        while (true)
        {
            float generationTime = Random.Range(ambientSoundSpawnTimePeriod.x, ambientSoundSpawnTimePeriod.y);
            yield return new WaitForSeconds(generationTime);

            Vector3 spawnPosition = GameManager.Manager.GetRandomPointNearPlayer(ambientSoundSpawnRadius);
            Instantiate(ambientSoundEmitterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
