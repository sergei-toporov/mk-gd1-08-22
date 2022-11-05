using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Game manager class.
 */
public class GameManager : MonoBehaviour
{
    /**
     * Manager instance.
     * 
     * @param GameManager
     */
    private static GameManager manager;
    public static GameManager Manager { get => manager; }

    /**
     * Player controller instance.
     * 
     * @param PlayerController
     */
    [SerializeField] private PlayerController player;
    public PlayerController Player {
        get
        {
            return player = player != null ? player : GetComponent<PlayerController>();
        }
    }

    /**
     * Camera controller instance.
     * 
     * @param CameraController
     */
    [SerializeField] private CameraController cameraController;
    public CameraController CameraController
    {
        get
        {
            return cameraController = cameraController != null ? cameraController : GetComponent<CameraController>();
        }
    }

    /**
     * UI Controller instance
     *
     * @param UIController
     */
    [SerializeField] private UIController uiController;
    public UIController UIController
    {
        get
        {
            return uiController = uiController != null ? uiController : GetComponent<UIController>();
        }
    }

    /**
     * {@inheritdoc}
     */
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
        Application.targetFrameRate = 60;

        foreach (SpawnerControllerBase spawnObject in FindObjectsOfType<SpawnerControllerBase>())
        {
            Instantiate(spawnObject.ObjectPrefab, spawnObject.transform.position, spawnObject.transform.rotation);
        }

        UIController.HideDeathScreen();
    }

    /**
     * {inheritdoc}
     */
    void Update()
    {
        if (Player.IsDead)
        {
            if (!UIController.DeathScreen.activeSelf)
            {
                UIController.ShowDeathScreen();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetGame();
            }
        }        
    }

    /**
     * Returns a quiet random boolean.
     * 
     * @return bool
     */
    public bool GetRandomBoolean()
    {
        return Random.value > 0.5f;
    }

    /**
     * Resets the game in case of players death.
     * 
     * @return void
     */
    private void ResetGame()
    {
        foreach (FoesControllerBase spawnedEnemy in FindObjectsOfType<FoesControllerBase>())
        {
            spawnedEnemy.transform.position = spawnedEnemy.StartPosition;
            if (spawnedEnemy.TryGetComponent(out Rigidbody2D spawnedEnemyRb))
            {
                spawnedEnemyRb.velocity = Vector2.zero;
            }
        }

        Player.Revive();
        UIController.HideDeathScreen();
    }
}
