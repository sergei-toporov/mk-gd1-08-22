using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMinicamera : MonoBehaviour
{
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
        this.GetComponent<Button>().onClick.AddListener(SwitchMiniCameraOnButton);
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        
    }

    /**
     * Switches minicamera.
     * 
     * @return void
     */
    private void SwitchMiniCameraOnButton()
    {
        int activeIndex = gameManager.GetActiveMiniCameraIndex();
        string buttonName = this.GetComponent<Button>().name;
        var triggeredCamera = gameManager.MiniCamerasPool.transform.Find(buttonName);
        if (triggeredCamera)
        {
            gameManager.MiniCamerasPool.transform.GetChild(activeIndex).GetComponent<Camera>().enabled = false;
            triggeredCamera.GetComponent<Camera>().enabled = true;
        }
    }
}
