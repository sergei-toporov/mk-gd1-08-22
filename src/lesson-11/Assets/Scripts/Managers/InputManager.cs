using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager manager;
    public static InputManager Manager { get => manager; }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Manager.GameState == GameManager.GameStates.InGame)
        {
            GameManager.Manager.InputResponse();
        }
    }
}
