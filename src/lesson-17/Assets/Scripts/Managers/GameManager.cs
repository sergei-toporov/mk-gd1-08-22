using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Game manager.
 */
public class GameManager : MonoBehaviour
{
    /**
     * List of possible game states.
     * 
     * @param enum
     */
    public enum GameStates
    {
        MainMenu = 0,
        InGame = 1,
    }

    /**
     * Current state of the game.
     * 
     * @param GameStates
     */
    private GameStates gameState;
    public GameStates GameState { get => gameState; }

    /**
     * GameManager instance.
     * 
     * @param GameManager
     */
    private static GameManager manager;
    public static GameManager Manager { get => manager; }

    /**
     * Player object.
     * 
     * @param PlayerController
     */
    [SerializeField] private PlayerController player;
    public PlayerController Player { get => player; }

    /**
     * Player's prefab object.
     * 
     * @param GameObject.
     */
    [SerializeField] private GameObject playerPrefab;

    /**
     * Player's spawn point.
     * 
     * @param PlayerSpawnPointController.
     */
    [SerializeField] private PlayerSpawnPointController playerSpawnPoint;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        InitializeManager();

        if (player == null)
        {
            SpawnPlayer();
        }

        gameState = GameStates.InGame;
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        
    }

    /**
     * Initializes game manager.
     * 
     * @return void
     */
    private void InitializeManager()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }
    }

    /**
     * Spawns player.
     * 
     * @return void
     */
    private void SpawnPlayer()
    {
        Vector3 spawnPosition = playerSpawnPoint.transform.position;
        // Think of replacement by collider size
        spawnPosition.y += 1.5f;
        var playerObject = Instantiate(playerPrefab, spawnPosition, playerSpawnPoint.transform.rotation);
        if (playerObject.TryGetComponent(out PlayerController spawnedPlayerController)) {
            player = spawnedPlayerController;
        }
        else
        {
            Debug.LogError("Player's prefab has no 'PlayerController' component. Please, add this component to prefab.");
            Application.Quit();
        }
    }

    public void ResetTargets()
    {
        TargetController[] targets = FindObjectsOfType<TargetController>();
        
        for (int index = 0; index < targets.Length; index++)
        {
            if (targets[index].TryGetComponent(out Rigidbody targetRb))
            {
                targetRb.velocity = Vector3.zero;

            }
            targets[index].transform.position = targets[index].StartPosition;
            targets[index].transform.rotation = targets[index].StartRotation;
        }
    }
}
