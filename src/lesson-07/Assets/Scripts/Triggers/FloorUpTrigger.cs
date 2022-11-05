using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FLoorUp trigger handler. Stores floor change direction flag.
 */
public class FloorUpTrigger : FloorTriggerBase
{
    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        directionFlag = GameManager.FloorUp;
    }
}
