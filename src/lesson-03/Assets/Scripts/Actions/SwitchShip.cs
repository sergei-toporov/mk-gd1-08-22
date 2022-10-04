using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/**
 * Active ships switching handler.
 */
public class SwitchShip : MonoBehaviour
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
        this.GetComponent<Button>().onClick.AddListener(SwitchShipOnButton);       
    }

    /**
     * Switches active ship.
     * 
     * @return void
     */
    private void SwitchShipOnButton()
    {
        int directionIndex = this.name == "Previous"
            ? GameManager.PrevShip
            : GameManager.NextShip;

        int currentActiveIndex = gameManager.GetActiveShipIndex();
        int newActiveIndex = 0;

        switch (directionIndex)
        {
            case GameManager.PrevShip:
                newActiveIndex = currentActiveIndex == 0
                    ? (gameManager.ShipPool.transform.childCount - 1)
                    : currentActiveIndex - 1;
                break;

            case GameManager.NextShip:
                newActiveIndex = currentActiveIndex == (gameManager.ShipPool.transform.childCount - 1)
                    ? 0
                    : currentActiveIndex + 1;
                break;
        }

        if (currentActiveIndex != newActiveIndex)
        {
            gameManager.ShipPool.transform.GetChild(currentActiveIndex).gameObject.SetActive(false);
            gameManager.ShipPool.transform.GetChild(newActiveIndex).gameObject.SetActive(true);
        }
    }
}
