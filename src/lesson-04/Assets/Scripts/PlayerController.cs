using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TSN_Utility;

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
     * @param GameObject[]
     */
    private GameObject[] cannons;

    /**
     * Currently active cannon.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject activeCannon;
    public GameObject ActiveCannon { get => activeCannon; }

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        if (activeCannon == null)
        {
            cannons = ObjectsFinder.FindChildrenInGameObjectByTag("RobotCannon", gameObject);
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
        activeCannon.GetComponent<CannonController>().MakeShot();
    }

    /**
     * Switches cannon according to the provided weapon switcher object.
     * 
     * If no such cannon in the collection, activates the very first cannon from the collection.
     * 
     * @param GameObject
     * 
     * @return void
     */
    public void SwitchCannon(GameObject weaponSwitcher)
    {
        var cannonIndex = weaponSwitcher.GetComponent<WeaponSwitcherController>().CannonType;
        activeCannon = (cannonIndex >= 0 && cannonIndex < cannons.Length)
            ? cannons[cannonIndex]
            : cannons[0];
    }
}
