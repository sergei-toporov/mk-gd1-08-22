using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Snakes controller.
 */
public class SnakeController : FoesControllerBase
{
    /**
     * Snakes rigidbody component.
     * 
     * @param Rigidbody2D
     */
    private Rigidbody2D snakeRb;
    public Rigidbody2D SnakeRb
    {
        get
        {
            return snakeRb = snakeRb != null ? snakeRb : GetComponent<Rigidbody2D>();
        }
    }

    /**
     * Snakes animator component.
     * 
     * @param Animator
     */
    private Animator snakeAnimator;
    public Animator SnakeAnimator
    {
        get
        {
            return snakeAnimator = snakeAnimator != null ? snakeAnimator : GetComponent<Animator>();
        }
    }

    /**
     * Snakes sprite renderer component.
     * 
     * @param SpriteRenderer
     */
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        }
    }

    /**
     * Snakes muscle force. Affects movement speed.
     * 
     * @param float.
     */
    [SerializeField] private float muscleForce = 100;

    /**
     * Range of possible snake movement range.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 movementLimitRange = new(5.0f, 15.0f);

    /**
     * Maximum distance from the start point for the snake's movement.
     * 
     * @param float
     */
    [SerializeField] private float movementLimit;

    /**
     * Movement direction flag.
     * 
     * @param int
     */
    private int directionFlag;

    /**
     * Position where the snake is moving at the moment.
     * 
     * @param Vector2
     */
    private Vector2 targetPosition;

    /**
     * Maximum snake velocity. Allows to keep snake movement within sane limits.
     * 
     * @param float.
     */
    private float maxVelocity = 2.0f;

    /**
     * Lowering coefficient for the snake's velocity.
     * 
     * @param float
     */
    private float velocityLoweringCoefficient = 0.8f;

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        SnakeAnimator.SetFloat("ForceX", muscleForce);
        movementLimit = Random.Range(movementLimitRange.x, movementLimitRange.y);
        directionFlag = GameManager.Manager.GetRandomBoolean() ? MoveLeft : MoveRight;
        SpriteRenderer.flipX = directionFlag == MoveRight;
        targetPosition = new(StartPosition.x + (movementLimit * directionFlag), StartPosition.y);
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if ((directionFlag == MoveLeft && targetPosition.x >= transform.position.x)
            || (directionFlag == MoveRight && targetPosition.x <= transform.position.x))
        {
            SwapTargetPosition();
        }
    }

    /**
     * {@inheritdoc}
     */
    private void FixedUpdate()
    {
        MoveCharacter();
    }

    /**
     * Moves the snake in some direction.
     *
     * @return void
     */
    private void MoveCharacter()
    {
        if (Mathf.Abs(SnakeRb.velocity.x) < maxVelocity)
        {
            SnakeRb.AddForce(Vector2.right * directionFlag * muscleForce, ForceMode2D.Force);
        }
        else
        {
            SnakeRb.velocity *= velocityLoweringCoefficient;
        }
    }

    /**
     * Swaps the snake's target position when it reaches the current target position.
     * 
     * @return void
     */
    private void SwapTargetPosition()
    {
        directionFlag *= MoveLeft;
        SpriteRenderer.flipX = !SpriteRenderer.flipX;
        targetPosition = new(StartPosition.x + (movementLimit * directionFlag), transform.position.y);
    }

    /**
     * Causes player to react on the snake's bite.
     * 
     * @param void.
     */
    protected override void TriggerAction()
    {
        GameManager.Manager.Player.ReactOnSnakeBite();
    }
}
