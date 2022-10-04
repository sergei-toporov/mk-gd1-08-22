using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Floor trigger base class.
 */
public class FloorTriggerBase : MonoBehaviour
{
    // Remove serialization after debugging.
    /**
     * Floor change direction flag. Equal to one of the constansts from the GameManager.
     * 
     * @param int
     */
    [SerializeField] protected int directionFlag;
    public int DirectionFlag { get => directionFlag; }

    /**
     * Floor trigger component.
     * 
     * @param FloorTriggerBase
     */
    protected FloorTriggerBase trigger;
    public FloorTriggerBase Trigger
    {
        get
        {
            return trigger = trigger != null ? trigger : GetComponent<FloorTriggerBase>();
        }
    }
    
    /**
     * Swaps floors if player touches the trigger.
     * 
     * @param Collider
     *   Collider object.
     *   
     * @return void
     */
    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController Player))
        {
            GameManager.Manager.SwapFloors(this);
        }
    }
}
