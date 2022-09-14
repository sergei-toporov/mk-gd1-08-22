using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    /**
     * Maximal degree camera can be turned per one swipe.
     * 
     * @param int
     */
    [SerializeField] [Range(0, 360)] private int maxCameraRotationPerSwipe = 90;

    /**
     * Camera rotation speed.
     * 
     * @param float
     */
    [SerializeField] [Range(1.0f, 20.0f)] private float maxRotationSpeed = 5.0f;

    /**
     * Camera object to orbit.
     * 
     * @param Camera
     */
    private Camera flybyCam;
    public Camera FlybyCam
    {
        set
        {
            if (value is Camera)
            {
                flybyCam = value;
            }
        }
    }

    /**
     * A target object to orbit around.
     * 
     * @param Transform
     */
    private Transform flybyCenter;
    public Transform FlybyCenter
    {
        get => flybyCenter;
    }

    /**
     * Previous position vector.
     * 
     * @param Vector3.
     */
    private Vector3 previousPosition;

    /**
     * Distance to the center to orbit around.
     * 
     * @param float
     */
    private float distanceToFlybyCenter = 10.0f;

    /**
     * Game Manager instance.
     * 
     * @param GameManager
     */
    private GameManager gameManager;

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        flybyCenter = gameManager.ShipPool.transform;
        distanceToFlybyCenter = Vector3.Distance(flybyCam.transform.position, flybyCenter.position);
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = flybyCam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = flybyCam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = (previousPosition - newPosition).normalized;
            float yAxisRotation = direction.x * maxCameraRotationPerSwipe * Time.deltaTime * maxRotationSpeed;
            
            flybyCam.transform.position = flybyCenter.position;
            flybyCam.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), yAxisRotation, Space.World);
            flybyCam.transform.Translate(new Vector3(0.0f, 0.0f, -distanceToFlybyCenter));

            previousPosition = newPosition;
        }
    }
}
