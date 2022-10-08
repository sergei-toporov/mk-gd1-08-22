using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handler of the game manager.
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
     * Main game field game object.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject gameField;
    public GameObject GameField { get => gameField; }

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
     * Returns a quiet random boolean value.
     * 
     * @return bool
     */
    public bool GetRandomBoolean()
    {
        return Random.value > 0.5f;
    }
}
