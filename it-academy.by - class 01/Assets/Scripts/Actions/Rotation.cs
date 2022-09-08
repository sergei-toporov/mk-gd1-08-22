using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object rotation.
 */ 
public class Rotation : ObjectActionBase
{
    /**
     * Rotation speed modifier.
     * 
     * @param float
     */
    [SerializeField] private float rotationSpeed = 5.0f;

    /**
     * Vector for storing the direction of the rotation.
     * 
     * @param Vector3
     */
    [SerializeField] private Vector3 rotationVector;

    /**
     * {@inheritdoc}
     */
    private void Start()
    {
        Vector3 rotationDirection = new Vector3(
            Random.Range(-90.0f, 90.0f),
            Random.Range(-90.0f, 90.0f),
            Random.Range(-90.0f, 90.0f)
            );
        rotationVector = new Vector3(
            rotationDirection.x * rotationSpeed * Time.deltaTime,
            rotationDirection.y * rotationSpeed * Time.deltaTime,
            rotationDirection.z * rotationSpeed * Time.deltaTime
            );
    }

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {
        transform.Rotate(rotationVector);
    }
}
