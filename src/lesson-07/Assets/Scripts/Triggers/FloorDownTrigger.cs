using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Floordown trigger. Stores a direction flag.
 */
public class FloorDownTrigger : FloorTriggerBase
{    
    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        directionFlag = GameManager.FloorDown;
    }
}
