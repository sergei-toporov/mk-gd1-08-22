using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * A handler for background level (layer).
 */
public class BackgroundLevelController : MonoBehaviour
{
    /**
     * Base background controller
     * 
     * @param BackgroundBaseController.
     */
    private BackgroundBaseController bgBaseController;

    /**
     * Layer controller.
     * 
     * @param BackgroundLevelController
     */
    private BackgroundLevelController controller;
    public BackgroundLevelController Controller
    {
        get
        {
            return controller = controller != null ? controller : GetComponent<BackgroundLevelController>();
        }
    }

    /**
     * Sprite renderer component.
     * 
     * @param SpriteRenderer
     */
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        }
    }

    /**
     * Paralax level for the layer.
     * 
     * @param float
     */
    [SerializeField] private float paralaxLevel;

    /**
     * Layer start position.
     * 
     * @param float
     */
    [SerializeField] private float startPosition;

    /**
     * Length of the layer's chunk.
     * 
     * @param float
     */
    [SerializeField] private float layerChunkLength;

    /**
     * {inheritdoc}
     */
    private void Start()
    {
        bgBaseController = GetComponentInParent<BackgroundBaseController>();
        paralaxLevel = SpriteRenderer.sortingOrder == 0 ? bgBaseController.MaxParalaxLevel : bgBaseController.MaxParalaxLevel - ((float) SpriteRenderer.sortingOrder / bgBaseController.BgLayersAmount);
        startPosition = transform.position.x;
        layerChunkLength = SpriteRenderer.bounds.size.x;
    }

    /**
     * {inheritdoc}
     */
    void FixedUpdate()
    {
        float distance = GameManager.Manager.CameraController.GameCamera.transform.position.x * paralaxLevel;
        float bgPosition = GameManager.Manager.CameraController.GameCamera.transform.position.x * (bgBaseController.MaxParalaxLevel - paralaxLevel);

        if (bgPosition > (startPosition + layerChunkLength))
        {
            startPosition += layerChunkLength;
        }
        else if (bgPosition < (startPosition - layerChunkLength))
        {
            startPosition -= layerChunkLength;
        }

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
    }
}
