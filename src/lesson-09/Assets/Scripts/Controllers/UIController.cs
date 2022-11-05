using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * UI controller class.
 */
public class UIController : MonoBehaviour
{
    /**
     * Death screen object.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject deathScreen;
    public GameObject DeathScreen { get => deathScreen; }

    /**
     * Hides the death screen.
     * 
     * @return void
     */
    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

    /**
     * Shows the death screen.
     * 
     * @return void.
     */
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
