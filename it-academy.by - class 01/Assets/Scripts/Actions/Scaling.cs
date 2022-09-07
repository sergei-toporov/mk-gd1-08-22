using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object scaling.
 */ 
public class Scaling : ObjectActionBase
{
    /**
     * Scaling multiplier.
     * 
     * Allows to make object smaller.
     * 
     * @param List<float>
     */
    [SerializeField] private List<float> scaleMultiplier = new List<float> { -1.5f, 1.5f };

    /**
     * Base X-axis scale.
     * 
     * @param float
     */
    [SerializeField] private float xAxis = 1.0f;

    /**
    * Base Y-axis scale.
    * 
    * @param float
    */
    [SerializeField] private float yAxis = 1.0f;

    /**
    * Base Z-axis scale.
    * 
    * @param float
    */
    [SerializeField] private float zAxis = 1.0f;

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {        
        float currentTimestamp = Time.time;
        if ((currentTimestamp - timestamp) > maxTimeDiff)
        {
            timestamp = currentTimestamp;
            xAxis = Random.Range(scaleMultiplier[0], scaleMultiplier[1]);
            yAxis = Random.Range(scaleMultiplier[0], scaleMultiplier[1]);
            zAxis = Random.Range(scaleMultiplier[0], scaleMultiplier[1]);
        }

        transform.localScale += new Vector3(
            xAxis * Time.deltaTime,
            yAxis * Time.deltaTime,
            zAxis * Time.deltaTime
            );
    }
}
