using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBController : SpawnableObjectsBase
{

    [SerializeField] Vector2 scaleBorders = new Vector2(0.1f, 2.5f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void TriggerAction()
    {
        transform.localScale = new Vector3(
            Random.Range(scaleBorders.x, scaleBorders.y),
            Random.Range(scaleBorders.x, scaleBorders.y),
            Random.Range(scaleBorders.x, scaleBorders.y)
            );
    }    
}
