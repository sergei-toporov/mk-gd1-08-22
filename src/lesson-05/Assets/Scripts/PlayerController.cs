using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Player controller.
 */
public class PlayerController : MonoBehaviour
{
    /**
     * Player's movement speed range.
     * 
     * @param float
     */
    [SerializeField][Range(0, 100)] private float movementSpeed = 10.0f;

    /**
     * Player's force of jump.
     * 
     * @param float
     */
    [SerializeField][Range(0.5f, 50.0f)] private float jumpForce = 5.0f;

    /**
     * Player's velocity vector.
     * 
     * @param Vector3
     */
    private Vector3 playerVelocity;

    /**
     * Amount of time to wait till coroutine logic repeat.
     * 
     * @param float
     */
    private float jumpCoroutineRepeatTime = 0.01f;

    /**
     * A timestamp of the start of the jump.
     * 
     * @param float
     */
    private float jumpTimestamp = 0.0f;

    /**
     * Character controller instance.
     * 
     * @param CharacterController
     */
    private CharacterController controller;
    public CharacterController Controller 
    { 
        get => controller = controller != null ? controller : GetComponent<CharacterController>();
    }

    /**
     * Assigned camera.
     * 
     * @param Camera
     */
    [SerializeField] private Camera playerCamera;
    public Camera PlayerCamera { get => playerCamera; }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (Controller.isGrounded)
        {
            playerVelocity.y = GameManager.Manager.GravityForce;
        }

        MoveByKeyboard();
        MoveByMouse();
    }

    /**
     * Movement by keyboard handler.
     * 
     * @return void
     */
    private void MoveByKeyboard()
    {
        playerVelocity.x = Input.GetAxis("Horizontal") * movementSpeed;
        playerVelocity.z = Input.GetAxis("Vertical") * movementSpeed;
        MakeMove();
        if (Input.GetKeyDown(KeyCode.Space) && Controller.isGrounded)
        {
            this.Jump();
        }
    }

    /**
     * Movement by mouse handler.
     * 
     * @return void
     */
    private void MoveByMouse()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            playerVelocity.z = movementSpeed;
            MakeMove();
        }

        if (Input.GetMouseButtonDown(1) && Controller.isGrounded && !EventSystem.current.IsPointerOverGameObject())
        {
            Jump();
        }
    }

    /**
     * Jumping handler.
     * 
     * @return void.
     */
    private void Jump()
    {
        playerVelocity.y = jumpForce * -GameManager.Manager.GravityForce;
        MakeMove();
        jumpTimestamp = Time.time;
        StartCoroutine(JumpCoroutine());
    }

    /**
     * Moves tha character with the CharacterController's Move().
     * 
     * @return void
     */
    private void MakeMove()
    {
        Controller.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime);
    }

    /**
     * Handles jumping process.
     * 
     * @return IEnumerator
     */
    private IEnumerator JumpCoroutine()
    {
        while (!Controller.isGrounded)
        {
            playerVelocity.y += GameManager.Manager.GravityForce * (Time.time - jumpTimestamp);
            yield return new WaitForSeconds(jumpCoroutineRepeatTime);
        }
    }
}
