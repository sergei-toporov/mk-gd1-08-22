using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Player Controller.
 */
public class PlayerController : MonoBehaviour
{
    /**
     * Character controller component.
     * 
     * @param CharacterController
     */
    private CharacterController controller;
    public CharacterController Controller
    {
        get => controller = controller != null ? controller : GetComponent<CharacterController>();
    }

    /**
     * Character camera.
     * 
     * @param Camera
     */
    private Camera characterCamera;
    public Camera CharacterCamera
    {
        get => characterCamera = characterCamera != null ? characterCamera : FindObjectOfType<Camera>();
    }

    /**
     * Character animator component.
     * 
     * @param CharacterAnimator.
     */
    private Animator characterAnimator;
    public Animator CharacterAnimator
    {
        get => characterAnimator = characterAnimator != null ? characterAnimator : GetComponent<Animator>();
    }

    /**
     * Movement speed value.
     * 
     * @param float
     */
    [SerializeField] private float movementSpeed = 1.0f;

    /**
     * Sprint speed value.
     * 
     * @param float
     */
    [SerializeField] private float sprintSpeed = 4.0f;

    /**
     * Rotation speed value.
     * 
     * @param float
     */
    [SerializeField] private float rotationSpeed = 3.0f;

    /**
     * Jump speed value.
     * 
     * @param float
     */
    [SerializeField] private float jumpSpeed = 20.0f;

    /**
     * Target speed for the Animator to play animations.
     * 
     * @param float
     */
    private float targetAnimatorSpeed = 0.0f;

    /**
     * Speed of animation blending.
     * 
     * @param float
     */
    private float animationBlendSpeed = 0.15f;

    /**
     * Vertical movement speed.
     * 
     * @param float
     */
    private float speedY;

    /**
     * Jumping state flag.
     * 
     * @param bool
     */
    private bool isJumping = false;

    /**
     * Sprinting state flag.
     * 
     * @param bool
     */
    private bool isSprinting = false;

    /**
     * Spawning state flag.
     * 
     * @param bool
     */
    private bool isSpawning = true;

    /**
     * Dead state flag
     * 
     * @param bool
     */
    private bool isDead = false;

    /**
     * Rotation anfle value.
     * 
     * @param float
     */
    private float rotationAngle = 0.0f;

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        // Check if character is in spawning state.
        if (isSpawning)
        {
            isSpawning = CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Spawn");
            if (!isSpawning)
            {
                CharacterAnimator.SetBool("IsSpawned", true);
            }
        }

        // Play dead or respawn.
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isDead)
            {
                isDead = false;
                isSpawning = true;
                CharacterAnimator.Play("Spawn");
            }
            else
            {
                CharacterAnimator.SetTrigger("Dead");
                isDead = true;
            }
        }

        // Do nothing more if character is in immovable state.        
        if (IsImmovableAnimatorState())
        {
            return;
        }

        /*
         * Hit in LMB.
         * 
         * @todo Find out how to check the name of substate machine.
         * @todo Get rid off magic range numbers. Find out if it's possible to count events
         *       from the substate machine by names.
         */
        if (Input.GetMouseButtonDown(0))
        {            
            CharacterAnimator.SetInteger("ComboNumber", Random.Range(1, 5));
            CharacterAnimator.SetTrigger("Attack");                       
        }

        // Character movement.
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            speedY += jumpSpeed;
        }

        if (!Controller.isGrounded)
        {
            speedY += GameManager.Manager.Gravity * Time.deltaTime;
        }
        else if (speedY < 0.0f)
        {
            speedY = 0.0f;
        }
        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);

        if (isJumping && speedY < 0.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        Vector3 movement = new Vector3(hAxis, 0.0f, vAxis);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;

        float currentSpeed = isSprinting ? sprintSpeed : movementSpeed;
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        Controller.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);

        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            targetAnimatorSpeed = isSprinting ? 1.0f : 0.5f;
        }
        else
        {
            targetAnimatorSpeed = 0.0f;
        }

        CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimatorSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }

    /**
     * Checks if the character is in one of immovable states.
     * 
     * @return bool
     *   TRUE - immovable state, FALSE - normal state.
     */
    private bool IsImmovableAnimatorState()
    {
        if (isSpawning)
        {
            return true;
        }

        if (isDead)
        {
            return true;
        }

        return false;
    }
}
