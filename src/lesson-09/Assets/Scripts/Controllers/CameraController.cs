using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Camera controller class.
 */
public class CameraController : MonoBehaviour
{
    /**
     * Camera instance.
     * 
     * @param Camera
     */
    protected Camera gameCamera;
    public Camera GameCamera { get => gameCamera; }

    /**
     * {@inheritdoc}
     */
    protected void Awake()
    {
        gameCamera = GetComponent<Camera>();
    }

    /**
     * Max distance from player, reaching which means camera starts to move.
     * 
     * @param float
     */
    private readonly float maxDistanceFromPlayerDefault = 5.0f;

    /**
     * Camera movement vector.
     * 
     * @param Vector3
     */
    private Vector3 tranlationVector = Vector3.zero;

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        UpdateTranslationVector();
    }

    /**
     * {@inheritdoc}
     */
    void FixedUpdate()
    {
        transform.Translate(tranlationVector * Time.fixedDeltaTime);
    }

    /**
     * Updates camera's movement vector.
     * 
     * @return void
     */
    private void UpdateTranslationVector()
    {
        float xDistanceToPlayer = transform.position.x - GameManager.Manager.Player.transform.position.x;
        float absXDistance = Mathf.Abs(xDistanceToPlayer);
        float directionFlag = xDistanceToPlayer / absXDistance;

        tranlationVector = Vector3.zero;
        if (maxDistanceFromPlayerDefault < absXDistance)
        {
            tranlationVector = directionFlag * GameManager.Manager.Player.MovementMultiplier * ((int)(absXDistance / maxDistanceFromPlayerDefault)) * Vector3.left;
        }
    }
}
