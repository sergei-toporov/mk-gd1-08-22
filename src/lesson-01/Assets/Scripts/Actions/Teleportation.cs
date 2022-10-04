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
    public void Start()
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
        while (true)
        {
            yield return new WaitForSeconds(maxTimeDiff);

            transform.position = new Vector3(
                Random.Range(borderMin.x, borderMax.x),
                Random.Range(borderMin.y, borderMax.y),
                Random.Range(borderMin.z, borderMax.z)
                );
        }        
    }
}
