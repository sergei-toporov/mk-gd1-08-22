using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player controller.
 */
public class PlayerController : MonoBehaviour
{

    /**
     * Movement speed value.
     * 
     * @param float
     */
    [SerializeField] private float movementSpeed = 50.0f;
    public float MovementSpeed { get => movementSpeed; }

    /**
     * Rotation speed value.
     * 
     * @param float
     */
    [SerializeField] private float rotationSpeed = 20.0f;
    public float RotationSpeed { get => rotationSpeed; }

    /**
     * Movement direction vector.
     * 
     * @param Vector3
     */
    private Vector3 movementDirection = Vector3.zero;

    /**
     * Rotation direction vector.
     * 
     * @param Vector3
     */
    private Vector3 rotationDirection = Vector3.zero;

    /**
     * Attached launchers.
     * 
     * @param LauncherController[]
     */
    private LauncherController[] attachedLaunchers;

    /**
     * Active launcher.
     * 
     * @param LauncherController
     */
    [SerializeField] private LauncherController activeLauncher;

    /**
     * If player has active launcher.
     * 
     * @param bool
     */
    private bool hasActiveLauncher = false;

    /**
     * {@inheritDoc}
     */
    private void Awake()
    {
        attachedLaunchers = gameObject.GetComponentsInChildren<LauncherController>();
        if (attachedLaunchers.Length > 0)
        {
            activeLauncher = attachedLaunchers[0];
            hasActiveLauncher = true;
        }
    }

    /**
     * {@inheritDoc}
     */
    private void FixedUpdate()
    {
        MoveCharacter();
    }

    /**
     * Moves/rotates character.
     * 
     * @return void
     */
    private void MoveCharacter()
    {
        if (movementDirection != Vector3.zero)
        {
            transform.Translate(Time.fixedDeltaTime * movementDirection);
        }

        if (rotationDirection != Vector3.zero)
        {
            transform.Rotate(Time.fixedDeltaTime * rotationDirection);
        }
    }

    /**
     * Processes input to determine player's behaviour.
     * 
     * @return void
     */
    public void ProcessInput()
    {
        float movement = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        movementDirection = movement == 0.0f ? Vector3.zero : movement > 0.0f ? Vector3.forward : Vector3.back;
        rotationDirection = rotation == 0.0f ? Vector3.zero : rotation > 0.0f ? Vector3.up : Vector3.down;

        movementDirection = movementDirection == Vector3.zero ? movementDirection : movementSpeed * movementDirection;
        rotationDirection = rotationDirection == Vector3.zero ? rotationDirection : rotationSpeed * rotationDirection;

        if (Input.GetKeyDown(KeyCode.Space) && hasActiveLauncher)
        {
            MakeShot();
        }
    }

    /**
     * Pulls the active launcher trigger.
     * 
     * @return void
     */
    private void MakeShot()
    {
        activeLauncher.Shoot();
    }

    /**
     * Switches active launcher on the provided one if it's in the player's launchers list.
     * 
     * @param LauncherController
     * 
     * @return void
     */
    public void TriggerWeaponSwitch(LauncherController switcherLauncher)
    {
        for (int index = 0; index < attachedLaunchers.Length; index++)
        {            
            if (attachedLaunchers[index].GetType() == switcherLauncher.GetType())
            {
                activeLauncher = attachedLaunchers[index];
                break;
            }
        }        
    }
}
