using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Game manager.
 */
public class GameManager : MonoBehaviour
{
    /**
     * Manager instance.
     * 
     * @param GameManager
     */
    [SerializeField] private static GameManager manager;
    public static GameManager Manager { get => manager; }

    /**
     * Player object instance.
     * 
     * @param GameObject
     */
    [SerializeField] GameObject player;
    public GameObject Player { get => player; }

    /**
     * PlayerController component of the @player.
     * 
     * @param PlayerController.
     */
    private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }

    /**
     * Gravity force value in terms of pushing. That's why it's negative.
     * 
     * @param float
     */
    [SerializeField] private float gravityForce = -9.81f;
    public float GravityForce { get => gravityForce; }

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }

        playerController = player.GetComponent<PlayerController>();
    }
}
