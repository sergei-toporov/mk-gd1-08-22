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
                Random.Range(borderMin.x, borderMax.x),
                Random.Range(borderMin.y, borderMax.y),
                Random.Range(borderMin.z, borderMax.z)
            );
            timestamp = currentTimestamp;
        }
    }
}
