using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object teleportation.
 */
public class Teleportation : ObjectActionBase
{

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {
        StartCoroutine(SpaceTimeJump());
    }

    /**
     * Launches the object teleportation each several seconds.
     * 
     * @return IEnumerator
     */
    protected IEnumerator SpaceTimeJump()
    {
        yield return new WaitForSeconds(maxTimeDiff);
        float currentTimestamp = Time.time;

        if ((currentTimestamp - timestamp) > maxTimeDiff)
        {
            transform.position = new Vector3(
            Random.Range(xBorder[0], xBorder[1]),
            1.0f,
            Random.Range(zBorder[0], zBorder[1])
            );
            timestamp = currentTimestamp;
        }
    }
}
