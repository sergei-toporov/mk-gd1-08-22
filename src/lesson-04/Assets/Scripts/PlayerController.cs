using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TSN_Utility;
using System.Linq;

/**
 * Player object handler.
 */
public class PlayerController : MonoBehaviour
{
    /**
     * Player's movement speed range.
     * 
     * @param float
     */
    [SerializeField] [Range(0, 100)] private float movementSpeed = 10.0f;

    /**
     * Player's rotation speed range.
     * 
     * @param float.
     */
    [SerializeField] [Range(0, 100)] private float rotationSpeed = 50.0f;

    /**
     * A collection of cannons connected to the player's game object.
     * 
     * @param CannonController[]
     */
    private CannonController[] cannons;

    /**
     * Currently active cannon.
     * 
     * @param CannonController
     */
    [SerializeField] private CannonController activeCannon;
    public CannonController ActiveCannon { get => activeCannon; }

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        if (activeCannon == null)
        {
            cannons = GetComponentsInChildren<CannonController>();
            activeCannon = cannons[Random.Range(0, cannons.Length)];
        }
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /**
     * Initiates a shot from the active cannon.
     * 
     * @return void
     */
    public void Shoot()
    {
        activeCannon.MakeShot();
    }

    /**
     * Switches cannon according to the provided cannon controller.
     * 
     * If no such cannon in the collection, activates the very first cannon from the collection.
     * 
     * @param CannonController
     * 
     * @return void
     */
    public void SwitchCannon(CannonController cannonController)
    {
        foreach (CannonController cannon in cannons)
        {
            if (cannon.name == cannonController.name)
            {
                activeCannon = cannon;
                return;
            }
        }

        activeCannon = cannons[0];
    }
}
