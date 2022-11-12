using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPlateController : PlateControllerBase
{
    private Vector3 movementDirection = Vector3.zero;

    private static float movementSpeed = 10.0f;

    private void Awake()
    {
        movementDirection = (GameManager.Manager.ActiveSpawner.transform.parent.transform.position - transform.position).normalized;
        movementSpeed += GameManager.Manager.PlateMovementSpeedDelta;
        Debug.Log(movementSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * movementDirection);
    }
}
