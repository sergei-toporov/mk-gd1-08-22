using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
 * Toggles handler.
 */
public class ToggleAction : MonoBehaviour
{
    /**
     * The "UI" object.
     * 
     * @param GameObject
     */
    protected GameObject canvas;

    /**
     * Current toggle instance.
     * 
     * @param Toggle
     */
    protected Toggle toggle;

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

        if (!(toggle = this.GetComponent<Toggle>()))
        {
            Debug.LogError("Wrong script assigment. Should be assigned on the 'Toggle' element.");
            Application.Quit();
        }

        toggle.onValueChanged.AddListener(delegate
            {
                OnToggleListener();
            }
        );

    }

    /**
     * Listens toggle event for the toggle element.
     * 
     * Displays concatenated names of enabled toggles within the message box.
     */
    protected void OnToggleListener()
    {
        var toggles = toggle.transform.parent.GetComponentsInChildren<Toggle>();

        /*
         * No toggles at all, no sense to do anything.
         */
        if (toggles.Length == 0)
        {
            return;
        }

        /*
         * Collect active toggles names.
         */
        var messageStrings = new string[toggles.Length];
        int counter = 0;
        foreach (var item in toggles)
        {
            if (!item.isOn)
            {
                continue;
            }

            messageStrings[counter++] = item.name;
        }

        /*
         * Clean empty values out.
         */
        messageStrings = messageStrings.Where(value => !string.IsNullOrEmpty(value)).ToArray();

        /*
         * Either display message box if there is any active toggle or hide it.
         */
        if (messageStrings.Length > 0)
        {
            canvas.GetComponent<UIBase>().MessageBox.SetActive(true);
            canvas.GetComponent<UIBase>().MessageBoxText.text = string.Join(" + ", messageStrings);
        }
        else
        {
            canvas.GetComponent<UIBase>().MessageBox.SetActive(false);
            canvas.GetComponent<UIBase>().MessageBoxText.text = "";
        }
    }
}
