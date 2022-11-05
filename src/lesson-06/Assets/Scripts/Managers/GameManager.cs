using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Game manager handler.
 */
public class GameManager : MonoBehaviour
{
    /**
     * Game manager instance.
     *
     * @param GameManager
     */
    private static GameManager manager;
    public static GameManager Manager { get => manager; }

    /**
     * Gravity force value.
     * 
     * @param float
     */
    [SerializeField] private float gravity = -9.81f;
    public float Gravity { get => gravity; }

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        manager = manager != this ? this : manager;
    }
}
