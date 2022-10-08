using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAController : SpawnableObjectsBase
{
    private Renderer objectRenderer;
    public Renderer ObjectRenderer
    {
        get
        {
            return objectRenderer = objectRenderer != null ? objectRenderer : GetComponent<Renderer>();
        }
    }

    private Vector2 colorRange = new(0.0f, 1.0f);
    private float alphaChannel = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /**
     * {@inheritdoc}
     */
    protected override void TriggerAction()
    {
        ObjectRenderer.material.SetColor("_Color", GetNewColor());
    }

    private Color GetNewColor()
    {
        return new Color(
            Random.Range(colorRange.x, colorRange.y),
            Random.Range(colorRange.x, colorRange.y),
            Random.Range(colorRange.x, colorRange.y),
            alphaChannel            
            );
    }
}
