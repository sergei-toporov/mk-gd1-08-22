using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles an object movement.
 * 
 * There are other ways to make it work.
 */ 
public class Movement : ObjectActionBase
{
    /**
     * Movement speed modifier.
     * 
     * @param float
     */
    [SerializeField] private float movementSpeed = 2.0f;

    /**
     * Vector for storing of the movement direction.
     * 
     * @param Vector3
     */
    [SerializeField] private Vector3 direction;
    
    /**
     * {@inheritdoc}
     */ 
    public void Start()
    {
        direction = new Vector3(Random.Range(0.5f, 1.0f), 0.0f, Random.Range(0.5f, 1.0f));
    }

    /**
     * {@inheritdoc}
     */ 
    protected override void MakeAction()
    {
        if (!this.CheckYPosition())
        {
            GameObject.Destroy(gameObject, 0.1f);
            return;
        }
        //this.AdjustGeneralPositioning();
        this.AdjustDirection();
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    /**
     * A safeguard for destroying the object, if it comes out of the Y-axis limits.
     */ 
    private bool CheckYPosition()
    {
        return (transform.position.y >= yBorder[0]) && (transform.position.y <= yBorder[1]);
    }

    /**
     * Adjusts the direction of movement.
     * 
     * @return void
     */
    private void AdjustDirection()
    {
        float xDirection, zDirection;
        xDirection = ((transform.position.x > (xBorder[0] + 0.5f)) && (transform.position.x < (xBorder[1] - 0.5f))) || (int) direction.x != 0
            ? direction.x
            : -direction.x * Random.Range(0.1f, 1.0f);

        zDirection = (transform.position.z > (zBorder[0] + 0.5f)) && (transform.position.z < (zBorder[1] - 0.5f)) || (int) direction.z != 0
            ? direction.z
            : -direction.z * Random.Range(0.1f, 1.0f);
        direction = new Vector3(xDirection, 0.0f, zDirection);
     }

    /**
     * Keeps the object within borders of X-axis and Z-axis.
     * 
     * @return void
     */
    private void AdjustGeneralPositioning()
    {
        float xPos = transform.position.x, yPos = transform.position.y, zPos = transform.position.z;

        xPos = xPos <= xBorder[0] ? xBorder[0] + 0.5f : xPos;
        xPos = xPos >= xBorder[1] ? xBorder[1] - 0.5f : xPos;

        zPos = zPos <= zBorder[0] ? zBorder[0] + 0.5f : zPos;
        zPos = zPos >= zBorder[1] ? zBorder[1] - 0.5f : zPos;

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
