using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ambient light handler for the randomly spawned light.
 */
public class AmbientLightEmitterController : MonoBehaviour
{
    /**
     * Light component.
     * 
     * @param Light
     */
    private Light ambientLight;

    /**
     * Time to live period for the spawned light.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 timeToLivePeriod = new(3.0f, 15.0f);

    /**
     * Is flickering light or not.
     * 
     * @param bool
     */
    [SerializeField] private bool isFlickering = false;

    /**
     * Flickering time.
     * 
     * @param float.
     */
    private float flickeringTime;

    /**
     * Time to live.
     * 
     * @param float
     */
    private float timeToLive;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        timeToLive = Random.Range(timeToLivePeriod.x, timeToLivePeriod.y);
        isFlickering = GameManager.Manager.GetRandomBoolean();
        ambientLight = GetComponent<Light>();
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        if (isFlickering)
        {
            StartCoroutine(FlickeringCoroutine());
        }

        StartCoroutine(LifeTimerCoroutine());
    }

    /**
     * Handles the light flickering.
     * 
     * @return IEnumerator
     */
    private IEnumerator FlickeringCoroutine()
    {
        while (true)
        {
            ambientLight.enabled = false;
            yield return new WaitForSeconds(flickeringTime);
            ambientLight.enabled = true;
        }
    }

    /**
     * Handles the light existance.
     * 
     * @return IEnumerator
     */
    private IEnumerator LifeTimerCoroutine()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
