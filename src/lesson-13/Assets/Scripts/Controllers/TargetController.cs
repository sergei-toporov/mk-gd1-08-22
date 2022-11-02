using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Target controller.
 */
public class TargetController : MonoBehaviour
{
    /**
     * Start position vector.
     * 
     * @param Vector3.
     */
    protected Vector3 startPosition;
    public Vector3 StartPosition { get => startPosition; }

    /**
     * Start rotation value.
     * 
     * @param Quaternion.
     */
    protected Quaternion startRotation;
    public Quaternion StartRotation { get => startRotation; }

    /**
     * {@inheritDoc}
     */
    protected void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
}
