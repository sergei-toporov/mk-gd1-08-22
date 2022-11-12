using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { MainMenu, InGame, GameOver }

    [SerializeField] private GameStates gameState;
    public GameStates GameState { get => gameState; }

    private static GameManager manager;
    public static GameManager Manager { get => manager; }

    private SpawnerController[] spawners;
    public SpawnerController[] Spawners { get => spawners; }

    private SpawnerController activeSpawner;
    public SpawnerController ActiveSpawner { get => activeSpawner; }

    [SerializeField] private TowerController towerController;
    public TowerController TowerController { get => towerController; }

    [SerializeField] private float plateMovementSpeedDelta = 1.0f;

    private bool activeSpawnedPlate = false;
    public bool ActiveSpawnedPlate { get => activeSpawnedPlate; }
    public float PlateMovementSpeedDelta { get => plateMovementSpeedDelta; }

    private void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }

        spawners = FindObjectsOfType<SpawnerController>();

        if (spawners.Length > 0)
        {
            activeSpawner = spawners[0];
        }
        else
        {
            Debug.LogError("No spawner objects on the scene!");
            Application.Quit();
        }

        gameState = GameStates.InGame;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameStates.GameOver && (!PlateGeneratorManager.Manager.IsGeneratingPossible || towerController.PlateMissed))
        {
            gameState = GameStates.GameOver;
        }
    }

    public void InputResponse()
    {
        if (!activeSpawnedPlate)
        {
            activeSpawnedPlate = true;
            PlateGeneratorManager.Manager.GenerateMovingPlate();
            TowerController.LowerTower();
        }
        else
        {
            activeSpawnedPlate = false;
            TowerController.AddPlate();
        }
    }    
}
