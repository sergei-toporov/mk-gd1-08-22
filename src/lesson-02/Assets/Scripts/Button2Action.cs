using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handler for the "Button 2" in the "Buttons" menu.
 */ 
public class Button2Action : MonoBehaviour
{
    /**
     * The "UI" object.
     * 
     * @param GameObject
     */
    protected GameObject canvas;

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        if ( !(canvas = GameObject.Find("UI"))) {
            Debug.LogError("The 'UI' element not found!");
            Application.Quit();
        }

        this.GetComponent<Button>().onClick.AddListener(OnClickListener);
        
    }

    /**
     * On button click listener.
     */
    protected void OnClickListener()
    {
        canvas.GetComponent<UIBase>().MessageBox.SetActive(true);
        canvas.GetComponent<UIBase>().MessageBoxText.text = this.name;
    }
}
