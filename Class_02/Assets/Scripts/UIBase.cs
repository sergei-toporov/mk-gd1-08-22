using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class for storing the UI basic configuration and elements.
 */
public class UIBase : MonoBehaviour
{
    /**
     * Message box object.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject messageBox;
    public GameObject MessageBox
    {
        get { return messageBox; }
    }

    /**
     * Message Box Text object.
     * 
     * @param TextMeshProUGUI
     */
    private TextMeshProUGUI messageBoxText;
    public TextMeshProUGUI MessageBoxText
    {
        get { return messageBoxText; }
    }

    /**
     * Main menu object.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject mainMenu;
    public GameObject MainMenu
    {
        get { return mainMenu; }
    }

    /**
     * {@inheritdoc}
     */ 
    void Start()
    {
        if (messageBox == null)
        {
            Debug.LogError("The 'MessageBox' element is not assigned in UI!");
            Application.Quit();
        }

        if (mainMenu == null)
        {
            Debug.LogError("The 'MainMenu' element is not assigned in UI!");
            Application.Quit();
        }

        messageBoxText = messageBox.GetComponentInChildren<TextMeshProUGUI>();
    }
}
