using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lesson3;
using Unity.VisualScripting;

/**
 * Game Management handler.
 */
public class GameManager : MonoBehaviour
{
    /**
     * Game UI object.
     * 
     * @param GameObject.
     */ 
    [SerializeField] private GameObject gameUI;
    public GameObject GameUI
    {
        get => gameUI;
    }

    /**
     * Ship prefabs collection.
     * 
     * @param GameObject[]
     */
    [SerializeField] private GameObject[] shipPrefabs;
    public GameObject[] ShipPrefabs
    {
        get => shipPrefabs;
    }

    /**
     * A pool of instantiated ships.
     * 
     * @param GameObject
     */
    private GameObject shipPool;
    public GameObject ShipPool
    {
        get => shipPool;
    }

    /**
     * A pool for minicameras.
     * 
     * @param GameObject
     */
    private GameObject miniCamerasPool;
    public GameObject MiniCamerasPool
    {
        get => miniCamerasPool;
    }

    /**
     * Vector to kepp euler angles for ship instantiatiing.
     * 
     * @param Vector3
     */
    private Vector3 startShipEulerAngles = new(160.0f, 0.0f, 180.0f);

    /**
     * Vector with start position for ship instantiating.
     * 
     * @param Vector3
     */
    private Vector3 startShipPosition = new(0.0f, 0.0f, 5.0f);

    /**
     * Start rotation for ship instantiating.
     * 
     * @param Quaternion
     */
    private Quaternion startShipRotation = Quaternion.identity;

    public const int PrevShip = -1;
    public const int NextShip = 1;

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        if (!CommonUtility.IsLaunchable(this))
        {
            Debug.LogError("An error occurred during the prelaunch check!");
            Application.Quit();
        }

        startShipRotation.eulerAngles = startShipEulerAngles;
        this.CreateShipPool();
        this.PrepareMiniCameras();
        this.InitializeInputController();
        
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        
    }

    /**
     * Creates a pool of instantiated ships.
     * 
     * @return void
     */
    private void CreateShipPool()
    {
        shipPool = new GameObject("ShipPool");
        foreach (GameObject prefab in shipPrefabs)
        {                  
            var ship = Instantiate(prefab, startShipPosition, startShipRotation);
            ship.transform.parent = shipPool.transform;
            ship.gameObject.SetActive(false);
        }

        shipPool.transform.GetChild(0).gameObject.SetActive(true);
    }

    /**
     * Enables the "Front" minicamera and disables all other minis.
     * 
     * @return void
     */
    private void PrepareMiniCameras()
    {
        miniCamerasPool = GameObject.Find("MiniCameras");
        foreach (Transform miniCamera in miniCamerasPool.transform)
        {
            miniCamera.GetComponent<Camera>().enabled = miniCamera.name == "Front";
        }
    }

    private void InitializeInputController()
    {
        GameObject inputController = new GameObject("InputController");
        inputController.AddComponent<OrbitCamera>();
        inputController.GetComponent<OrbitCamera>().FlybyCam =
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    /**
     * Returns an index of the currently active ship.
     * 
     * @return int
     *   Active ship index.
     */
    public int GetActiveShipIndex()
    {
        int activeIndex = 0;
        foreach (Transform ship in shipPool.transform)
        {
            if (ship.gameObject.activeSelf)
            {
                return ship.GetSiblingIndex();
            }
            
        }

        return activeIndex;
    }

    /**
     * Returns an index of the currently active minicamera.
     * 
     * @return int
     *   Active minicamera index.
     */
    public int GetActiveMiniCameraIndex()
    {
        int activeIndex = 0;
        foreach (Transform miniCamera in miniCamerasPool.transform)
        {
            if (miniCamera.GetComponent<Camera>().enabled)
            {
                return miniCamera.GetSiblingIndex();
            }

        }

        return activeIndex;
    }
}
