using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BackgroundBaseController : MonoBehaviour
{
    private BackgroundBaseController controller;
    public BackgroundBaseController Controller
    {
        get
        {
            return controller = controller != null ? controller: GetComponent<BackgroundBaseController>();
        }
    }

    [SerializeField] private int bgLayersAmount;
    public int BgLayersAmount { get => bgLayersAmount; }

    private int maxParalaxLevel = 1;
    public int MaxParalaxLevel { get => maxParalaxLevel; }

    private void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
