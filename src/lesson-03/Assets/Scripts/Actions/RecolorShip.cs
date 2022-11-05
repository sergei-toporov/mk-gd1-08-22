using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Ship recoloring button handler.
 */
public class RecolorShip : MonoBehaviour
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
        this.GetComponent<Button>().onClick.AddListener(RecolorShipOnButton);

    }

    /**
     * Recolors active ship.
     * 
     * @return void
     */
    private void RecolorShipOnButton()
    {
        int activeIndex = gameManager.GetActiveShipIndex();
        Color buttonColor = this.GetComponent<Button>().image.color;
        foreach (Transform part in gameManager.ShipPool.transform.GetChild(activeIndex).transform)
        {
            if (!part.GetComponent<Renderer>().material) {
                continue;
            }
            part.GetComponent<Renderer>().material.SetColor("_Color", buttonColor);
        }
    }
}
