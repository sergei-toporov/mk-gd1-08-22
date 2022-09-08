using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object movement.
 * 
 * There are other ways to make it work.
 */ 
public class Movement : ObjectActionBase
{
    /**
     * Movement speed modifier.
     * 
     * @param float
     */
    [SerializeField] private float movementSpeed = 2.0f;

    /**
     * Start position vector.
     * 
     * @param Vector3
     */ 
    [SerializeField] private Vector3 startPosition;

    /**
     * Target position vector.
     * 
     * @param Vector3
     */ 
    [SerializeField] private Vector3 targetPosition;
    
    /**
     * {@inheritdoc}
     */ 
    public void Start()
    {
        startPosition = new Vector3(
            Random.Range(borderMin.x, borderMax.x),
            Random.Range(borderMin.y, borderMax.y),
            Random.Range(borderMin.z, borderMax.z)
            );

        targetPosition = new Vector3(-startPosition.x, startPosition.y, -startPosition.z);
    }

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            movementSpeed * Time.deltaTime
            );

        if (transform.position == targetPosition)
        {
            (targetPosition, startPosition) = (startPosition, targetPosition);
        }
    }
}
