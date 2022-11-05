using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Input manager.
 */
public class InputManager : MonoBehaviour
{
    public enum KeyActions
    {
        Pressed,
        PressedDown,
        Released,
    }

    /**
     * Manager instance
     * 
     * @param InputManager
     */
    private static InputManager manager;
    public static InputManager Manager { get => Manager; }

    /**
     * A reference on the GameManager instance.
     * 
     * @param GameManager
     */
    private GameManager gameManager;

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

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Manager;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState == GameManager.GameStates.InGame)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManager.Manager.ResetTargets();
            }
            else
            {
                gameManager.Player.ProcessInput();
            }            
        }
    }    
}
