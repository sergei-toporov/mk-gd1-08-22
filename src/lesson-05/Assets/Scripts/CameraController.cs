using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Camera controller.
 */
public class CameraController : MonoBehaviour
{
    /**
     * Rotation direction determiner.
     * 
     * @const int
     * 
     * @todo Split it to make separate directions.
     */
    public const int RotateLeftOrDown = 1;

    /**
     * Rotation direction determiner.
     * 
     * @const int
     */
    public const int RotateRightOrUp = -1;

    /**
     * Rotation direction determiner.
     * 
     * @const int
     */
    private const int RotateNull = 0;

    /**
     * Flag to keep the direction of camera rotation during the work.
     * 
     * @param int
     */
    private int rotationDirection = 0;

    /**
     * Flag to keep the direction of vertical rotation during the work.
     * 
     * @param int.
     */
    private int pitchDirection = 0;

    /**
     * Vector for storing of a previous position.
     *
     * @param Vector3
     */
    private Vector3 previousPosition;

    /**
     * Maximal degree camera can be turned per one swipe.
     * 
     * @param int
     */
    [SerializeField][Range(0, 360)] private int maxCameraRotationPerSwipe = 90;

    /**
     * Camera rotation speed.
     * 
     * @param float
     */
    [SerializeField][Range(1.0f, 50.0f)] private float maxRotationSpeed = 50.0f;

    /**
     * Camera object instance.
     * 
     * @param Camera
     */
    private Camera controller;
    public Camera Controller { get => controller = controller != null ? controller : GetComponent<Camera>(); }

    /**
     * Vector for storing extremums for camera pitching.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 viewAngleExtremums = new(-60, 40);

    /**
     * Vector for storing of the current camera rotation numbers.
     * 
     * @param Vector3
     */
    private Vector3 currentRotation;

    

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        currentRotation = new (transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        this.DoKeyboardControl();
        this.DoMouseControl();
    }

    /**
     * Handles keyboard control.
     * 
     * @return void
     */
    private void DoKeyboardControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationDirection = RotateLeftOrDown;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotationDirection = RotateRightOrUp;
        }

        if (rotationDirection != 0 && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)))
        {
            transform.parent.transform.Rotate(Vector3.up, maxRotationSpeed * Time.deltaTime * rotationDirection);
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            rotationDirection = RotateNull;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pitchDirection = RotateLeftOrDown;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pitchDirection = RotateRightOrUp;
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.F))
        {
            PitchCamera(pitchDirection);
        }
    }

    /**
     * Handles mouse control.
     * 
     * @return void
     */
    private void DoMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = Controller.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 newPosition = Controller.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = (previousPosition - newPosition).normalized;
                float yAxisRotation = direction.x * maxCameraRotationPerSwipe * Time.deltaTime;

                transform.parent.transform.Rotate(Vector3.up, yAxisRotation);
                previousPosition = newPosition;
            }            
        }
    }

    /**
     * Pitches camera according to the provided direction flag.
     * 
     * @param int providedPitchDirection.
     *   One of the values: -1, 1.
     * 
     * @return void
     * 
     * @todo Maybe it'll be better to add another check for the flag values.
     */
    public void PitchCamera(int providedPitchDirection)
    {        
            currentRotation += new Vector3(providedPitchDirection * maxRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            currentRotation.x = Mathf.Clamp(currentRotation.x, viewAngleExtremums.x, viewAngleExtremums.y);
            transform.localEulerAngles = currentRotation;        
    }
}
