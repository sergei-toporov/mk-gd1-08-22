using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Base class for hostile entities.
 */
public class FoesControllerBase : SpawnableBase
{
    /**
     * Flag for the movement to the left.
     *
     * @const int
     */
    public const int MoveLeft = -1;

    /**
     * Flag for the movement to the right.
     * 
     * @const int
     */
    public const int MoveRight = 1;

    /**
     * Controller instance.
     * 
     * @param FoesControllerBase
     */
    protected FoesControllerBase controller;

    public FoesControllerBase Controller
    {
        get
        {
            return controller = controller != null ? controller : GetComponent<FoesControllerBase>();
        }
    }

    /**
     * {@inheritdoc}
     */
    protected override void Awake()
    {
        base.Awake();
    }

    /**
     * Triggers action if collides with player.
     * 
     * @return void
     */
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController Player))
        {
            TriggerAction();
        }
    }

    /**
     * Entity specific action.
     * 
     * @return void
     */
    protected virtual void TriggerAction()
    {

    }
}
