using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * "Back" buttons handler.
 */
public class BackButtonAction : MonoBehaviour
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
        if (!(canvas = GameObject.Find("UI")))
        {
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
        canvas.GetComponent<UIBase>().MainMenu.SetActive(true);
        canvas.GetComponent<UIBase>().MessageBox.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
    }
}
