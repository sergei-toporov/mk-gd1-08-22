using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Orbital camera handler.
 */
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
    private Camera orbitalCamera;
    public Camera OrbitalCamera
    {
        set
        {
            orbitalCamera = value;
        }
    }

    /**
     * A target object to orbit around.
     * 
     * @param Transform
     */
    private Transform orbitCenter;
    public Transform OrbitCenter
    {
        get => orbitCenter;
    }

    /**
     * A game object for camera positioning.
     * 
     * @param GameObject
     */
    private GameObject orbitalCameraRay;

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
    private float distanceToOrbitCenter;

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
        orbitCenter = gameManager.ShipPool.transform;
        distanceToOrbitCenter = Vector3.Distance(orbitalCamera.transform.position, orbitCenter.position);
        orbitalCameraRay = new GameObject("OrbitCameraRay");
        orbitalCameraRay.transform.position = orbitalCamera.transform.position;
        orbitalCameraRay.transform.rotation = orbitalCamera.transform.rotation;
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = orbitalCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = orbitalCamera.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = (previousPosition - newPosition).normalized;
            float yAxisRotation = direction.x * maxCameraRotationPerSwipe * Time.deltaTime * maxRotationSpeed;

            orbitalCameraRay.transform.position = orbitCenter.position;
            orbitalCameraRay.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), yAxisRotation, Space.World);
            orbitalCameraRay.transform.Translate(new Vector3(0.0f, 0.0f, -distanceToOrbitCenter));

            orbitalCamera.transform.position = orbitalCameraRay.transform.position;
            orbitalCamera.transform.rotation = orbitalCameraRay.transform.rotation;
            previousPosition = newPosition;
        }
    }
}
