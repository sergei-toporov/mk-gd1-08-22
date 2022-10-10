using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

/**
 * Handler for the background root object controller.
 */
public class BackgroundBaseController : MonoBehaviour
{
    /**
     * Controller instance.
     * 
     * @param BackgroundBaseController
     */
    private BackgroundBaseController controller;
    public BackgroundBaseController Controller
    {
        get
        {
            return controller = controller != null ? controller: GetComponent<BackgroundBaseController>();
        }
    }

    /**
     * Amount of background layers. Used in paralax level calculation.
     * 
     * @param int
     */
    private int bgLayersAmount;
    public int BgLayersAmount { get => bgLayersAmount; }

    /**
     * Max paralax level. 
     * 
     * @param int
     */
    private int maxParalaxLevel = 1;
    public int MaxParalaxLevel { get => maxParalaxLevel; }

    /**
     * {inheritdoc}
     */
    private void Start()
    {    
        GameObject[] bgLayers = GameObject.FindGameObjectsWithTag("Background_Layer");
        bgLayersAmount = bgLayers.Length;
        if (bgLayersAmount == 0)
        {
            Debug.LogError("No background layers found! Can't proceed with work!");
            Utils.ForceCrash(ForcedCrashCategory.Abort);
        }

        foreach (GameObject layer in bgLayers)
        {
            layer.AddComponent<BackgroundLevelController>();
        }
    }
}
