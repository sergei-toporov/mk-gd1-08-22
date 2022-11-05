using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A base class for spawnable things.
 */
public abstract class SpawnableBase : MonoBehaviour
{
    /**
     * Spawned object start position.
     * Used in game reset.
     * 
     * @param Vector2
     */
    protected Vector2 startPosition;
    public Vector2 StartPosition { get => startPosition; }

    /**
     * Is spawned object static or not.
     * 
     * @param bool
     */
    protected bool isStatic;
    public bool IsStatic { get => isStatic; }

    /**
     * Marks if start position were set or not.
     * 
     * @param bool
     */
    protected bool isStartPositionSet = false;

    /**
     * {@inheritdoc}
     */
    protected virtual void Awake()
    {
        if (!isStartPositionSet)
        {
            startPosition = transform.position;
            isStartPositionSet = true;
        }
    }
}
