using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Player Controller.
 * 
 * @todo refactor it. At the moment it's a real mess.
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
     * Player's audio controller component.
     * 
     * @param PlayerAudioController
     */
    private PlayerAudioController playerAudio;

    public PlayerAudioController PlayerAudio
    {
        get
        {
            return playerAudio = playerAudio != null ? playerAudio : GetComponent<PlayerAudioController>();
        }
    }

    /**
     * Movement speed value.
     * 
     * @param float
     */
    [SerializeField] private float movementSpeed = 10.0f;

    /**
     * Rotation speed value.
     * 
     * @param float
     */
    [SerializeField] private float characterRotationSpeed = 30.0f;

    /**
     * Camera rotation speed values: x-axis, y-axis.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 cameraRotationSpeeds = new(200.0f, 200.0f);

    /**
     * Camera rotation values.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 cameraRotationValues = Vector2.zero;

    /**
     * Horizontal axis input value.
     * 
     * @param float
     */
    private float hAxis;

    /**
     * Vertical axis input value.
     * 
     * @param float
     */
    private float vAxis;

    /**
     * Vertical movement speed.
     * 
     * @param float
     */
    private float speedY;

    /**
     * Rotation anfle value.
     * 
     * @param float
     */
    private float rotationAngle = 0.0f;

    /**
     * {@inheritdoc}
     */
    private void Start()
    {
        StartCoroutine(PlayFootstepsCoroutine());
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        // Character movement.
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
        cameraRotationValues += new Vector2(-cameraRotationSpeeds.x * mouseY, cameraRotationSpeeds.y * mouseX);
        CharacterCamera.transform.eulerAngles = cameraRotationValues;

        if (!Controller.isGrounded)
        {
            speedY += GameManager.Manager.Gravity * Time.deltaTime;
        }
        else if (speedY < 0.0f)
        {
            speedY = 0.0f;
        }

        Vector3 movement = new Vector3(hAxis, 0.0f, vAxis);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;

        Controller.Move((verticalMovement + rotatedMovement * movementSpeed) * Time.deltaTime);


        rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, characterRotationSpeed);
    }

    /**
     * Calls for footstep sounds.
     * 
     * @todo think about implementation playing by distance.
     * 
     * @return IEnumerator.
     */
    private IEnumerator PlayFootstepsCoroutine()
    {
        while (true)
        {
            if (vAxis != 0.0f || hAxis != 0.0f)
            {
                PlayerAudio.PlayFootstep();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
