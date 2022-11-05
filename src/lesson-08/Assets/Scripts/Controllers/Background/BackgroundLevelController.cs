using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundLevelController : MonoBehaviour
{
    private BackgroundBaseController bgBaseController;

    private BackgroundLevelController controller;
    public BackgroundLevelController Controller
    {
        get
        {
            return controller = controller != null ? controller : GetComponent<BackgroundLevelController>();
        }
    }

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        }
    }

    [SerializeField] private float paralaxLevel;
    [SerializeField] private float startPosition;
    [SerializeField] private float layerChunkLength;

    private void Start()
    {
        bgBaseController = GetComponentInParent<BackgroundBaseController>();
        paralaxLevel = SpriteRenderer.sortingOrder == 0 ? bgBaseController.MaxParalaxLevel : bgBaseController.MaxParalaxLevel - ((float) SpriteRenderer.sortingOrder / bgBaseController.BgLayersAmount);
        startPosition = transform.position.x;
        layerChunkLength = SpriteRenderer.bounds.size.x;
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        float distance = GameManager.Manager.Player.transform.position.x * paralaxLevel;
        float bgPosition = GameManager.Manager.Player.transform.position.x * (bgBaseController.MaxParalaxLevel - paralaxLevel);

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
