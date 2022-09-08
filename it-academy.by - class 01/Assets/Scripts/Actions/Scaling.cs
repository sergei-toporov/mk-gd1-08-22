using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object scaling.
 */ 
public class Scaling : ObjectActionBase
{
    /**
     * Scaling vector.
     * 
     * @param Vector3
     */ 
    [SerializeField] private Vector3 scalingVector = new Vector3(1.0f, 1.0f, 1.0f);

    /**
     * Scaling vector extremums.
     * X - minimal value, Y - maximal value.
     *  
     * @param Vector2 
     */ 
    [SerializeField] private Vector2 vectorExtremums = new Vector2(-1.5f, 1.5f);

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {        
        float currentTimestamp = Time.time;
        if ((currentTimestamp - timestamp) > maxTimeDiff)
        {
            timestamp = currentTimestamp;
            scalingVector = new Vector3(
                Random.Range(vectorExtremums.x, vectorExtremums.y),
                Random.Range(vectorExtremums.x, vectorExtremums.y),
                Random.Range(vectorExtremums.x, vectorExtremums.y)                
                );
        }

        transform.localScale += new Vector3(
            scalingVector.x * Time.deltaTime,
            scalingVector.y * Time.deltaTime,
            scalingVector.z * Time.deltaTime
            );
    }
}
