using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Player controller class
 */
public class PlayerController : SpawnableBase
{
    /**
     * Controller instance.
     * 
     * @param PlayerController
     */
    private PlayerController controller;
    public PlayerController Controller
    {
        get
        {
            return controller = controller != null ? controller : GetComponent<PlayerController>();
        }
    }

    /**
     * Sprite renderer component.
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
     * Animator component.
     * 
     * @param Animator.
     */
    private Animator characterAnimator;
    public Animator CharacterAnimator
    {
        get
        {
            return characterAnimator = characterAnimator != null ? characterAnimator : GetComponent<Animator>();
        }
    }

    /**
     * Rigidbody component.
     * 
     * @param Rigidbody2D
     */
    private Rigidbody2D playerRb;
    public Rigidbody2D PlayerRb
    {
        get
        {
            return playerRb = playerRb != null ? playerRb : GetComponent<Rigidbody2D>();
        }
    }

    /**
     * Collider comonent.
     * 
     * @param Collider2D
     */
    private Collider2D playerCollider;
    public Collider2D PlayerCollider
    {
        get
        {
            return playerCollider = playerCollider != null ? playerCollider : GetComponent<Collider2D>();
        }
    }

    /**
     * Movement speed multiplier. For fine tuning.
     * 
     * @param float
     */
    [SerializeField][Range(0.0f, 15.0f)] private float movementMultiplier = 10.0f;
    public float MovementMultiplier { get => movementMultiplier; }

    /**
     * Default muscle force.
     * 
     * @param float
     */
    [SerializeField] private float defaultMuscleForce = 4000.0f;

    /**
     * Current muscle force. Have its use in movement calculations.
     * 
     * @param float
     */
    private float currentMuscleForce;

    /**
     * Jump muscle force.
     * 
     * @param float
     */
    [SerializeField] private float jumpForce = 8000.0f;

    /**
     * Jump force multiplier. For fine tuning.
     * 
     * @param float
     */
    [SerializeField] private float jumpForceMultiplier = 50.0f;
    
    /**
     * Character skin thickness.
     * 
     * @param float
     */
    private float skinThickness = 0.1f;

    /**
     * Movement direction vector.
     * 
     * @param Vector2
     */
    private Vector2 movementDirection = Vector2.zero;

    /**
     * Flags if character is dead or alive.
     * 
     * @param bool
     */
    private bool isDead = false;
    public bool IsDead { get => isDead; }

    /**
     * Flags if character is grounded or not.
     * 
     * @param bool
     */
    public bool IsGrounded
    {
        get
        {
            Vector2 currentPosition = transform.position;
            currentPosition.y = PlayerCollider.bounds.min.y - skinThickness;
            bool grounded = Physics2D.Raycast(currentPosition, Vector2.down, skinThickness);
            return grounded;
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
     * {@inheritdoc}
     */
    void Start()
    {
        currentMuscleForce = defaultMuscleForce;
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (!isDead)
        {
            ProcessInput();
        }

    }

    /**
     * {@inheritdoc}
     */
    private void FixedUpdate()
    {
        if (!isDead)
        {
            MoveCharacter();
        }               
    }

    /**
     * Processes input.
     * 
     * @return void
     */
    private void ProcessInput()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = 0.0f;
        
        if ((!SpriteRenderer.flipX && hAxis < 0) || (SpriteRenderer.flipX && hAxis > 0))
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vAxis = 1.0f;
        }

        movementDirection = new Vector2(hAxis, vAxis);
        CharacterAnimator.SetFloat("ForceX", (currentMuscleForce / movementMultiplier) * Mathf.Abs(hAxis));
    }

    /**
     * Moves character.
     * 
     * @todo: actually, it can be moved into the base class.
     * 
     * @return void
     */
    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(
            movementDirection.x * MovementMultiplier * currentMuscleForce,
            movementDirection.y * jumpForce * jumpForceMultiplier
            );
        PlayerRb.AddForce(movement, ForceMode2D.Force);        
    }

    /**
     * Handles the reaction on snake bite.
     * 
     * @return void
     */
    public void ReactOnSnakeBite()
    {
        isDead = true;
        CharacterAnimator.SetFloat("ForceX", 0);
        SpriteRenderer.flipY = true;
    }

    /**
     * Returns character to life.
     * 
     * @return void
     */
    public void Revive()
    {
        isDead = false;
        SpriteRenderer.flipY = false;
        transform.position = StartPosition;
    }

    /**
     * Slows character with provided force for provided time.
     * 
     * @param effectTime
     *   How long the character will be affected.
     *   
     * @param effectForce
     *   How strong the character will be affected.
     *   
     * @return IEnumerator.
     */
    public IEnumerator SlowDownCharacterCoroutine(float effectTime, float effectForce)
    {
        currentMuscleForce = effectForce != 0
            ? defaultMuscleForce / effectForce
            : defaultMuscleForce;

        yield return new WaitForSeconds(effectTime);
        currentMuscleForce = defaultMuscleForce;
    }
}
