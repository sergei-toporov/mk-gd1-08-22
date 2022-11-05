using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Structure for keeping swappable ladders.
 */
[System.Serializable] public struct SwappableLadder
{
    /**
     * Ladder prefab.
     * 
     * @param GameObject.
     */
    public GameObject Prefab;

    /**
     * Vertical position value.
     * 
     * @param float
     */
    public float VerticalPosition;

    /**
     * Constructor for the structure.
     */
    public SwappableLadder(GameObject prefab, float verticalPosition)
    {
        Prefab = prefab;
        VerticalPosition = verticalPosition;
    }    
}

/**
 * Game manager handler.
 */
public class GameManager : MonoBehaviour
{
    /**
     * Floor up flag.
     * 
     * @const int
     */
    public const int FloorUp = 1;

    /**
     * Floor down flag.
     * 
     * @const int
     */
    public const int FloorDown = -1;

    /**
     * Previous ladder object index in the ladders structure array.
     * 
     * @param int
     */
    private readonly int prevLadderIndex = 0;

    /**
     * Current ladder object index in the ladders structure array.
     * 
     * @param int
     */
    private readonly int currentLadderIndex = 1;

    /**
     * Next ladder object index in the ladders structure array.
     * 
     * @param int
     */
    private readonly int nextLadderIndex = 2;

    /**
     * Game manager instance.
     *
     * @param GameManager
     */
    private static GameManager manager;
    public static GameManager Manager { get => manager; }

    /**
     * Player object prefab.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerObject;

    /**
     * Player Controller component.
     * 
     * @param PlayerComponent
     */
    private PlayerController player;
    public PlayerController Player { get => player; }

    /**
     * Player spawn point object.
     * 
     * @param GameObject
     */
    [SerializeField] private GameObject playerSpawnPoint;
    

    /**
     * Gravity force value.
     * 
     * @param float
     */
    [SerializeField] private float gravity = -9.81f;
    public float Gravity { get => gravity; }

    // remove serialization after debug :)
    /**
     * Current floor number.
     * 
     * @param int
     */
    [SerializeField] private int floorNumber = 1;

    /**
     * Swappable ladders array
     * 
     * @param SwappableLadder[]
     */
    [SerializeField] SwappableLadder[] swappableLadders;

    /**
     * Floor trigger object.
     * 
     * @param FloorTriggerBase
     */
    private FloorTriggerBase floorTrigger;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        manager = manager != this ? this : manager;
        playerObject = Instantiate(playerPrefab, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
        playerObject.transform.SetParent(swappableLadders[prevLadderIndex].Prefab.transform, true);
        player = playerObject.GetComponent<PlayerController>();
    }

    /**
     * Proceeds with floors swapping.
     * 
     * @param FloorTriggerBase
     *   A trigger instance.
     * 
     * @return void
     */
    public void SwapFloors(FloorTriggerBase trigger)
    {
        floorTrigger = trigger;
        Debug.Log(floorTrigger.name + " / " + floorTrigger.DirectionFlag);
        switch (trigger.DirectionFlag)
        {
            case FloorUp:
                floorNumber++;
                RollFloors();
                break;

            case FloorDown:
                if (floorNumber == 1)
                {
                    floorNumber++;
                    RepositionPlayer();
                    return;
                }

                if (floorNumber == 2)
                {
                    floorNumber--;
                    return;
                }

                floorNumber--;

                RollFloors();
                break;
        }


    }

    /**
     * Handles floor swapping.
     * 
     * @return void
     */
    private void RollFloors()
    {
        ResortLaddersArray();
        RepositionPlayer();
        Player.Controller.enabled = false;
        foreach (SwappableLadder ladder in swappableLadders)
        {
            ladder.Prefab.transform.position = new Vector3(
                ladder.Prefab.transform.position.x,
                ladder.VerticalPosition,
                ladder.Prefab.transform.position.z
                );
        }
        Player.Controller.enabled = true;
        
    }

    /**
     * Resorts array of swappable ladders.
     * 
     * @return void.
     */
    private void ResortLaddersArray()
    {
        SwappableLadder[] tmpLadders = new SwappableLadder[swappableLadders.Length];
        if (floorTrigger.DirectionFlag == FloorUp)
        {
            tmpLadders[prevLadderIndex] = new (swappableLadders[currentLadderIndex].Prefab, swappableLadders[prevLadderIndex].VerticalPosition);
            tmpLadders[currentLadderIndex] = new (swappableLadders[nextLadderIndex].Prefab, swappableLadders[currentLadderIndex].VerticalPosition);
            tmpLadders[nextLadderIndex] = new (swappableLadders[prevLadderIndex].Prefab, swappableLadders[nextLadderIndex].VerticalPosition);
        }
        else
        {
            tmpLadders[prevLadderIndex] = new(swappableLadders[nextLadderIndex].Prefab, swappableLadders[prevLadderIndex].VerticalPosition);
            tmpLadders[currentLadderIndex] = new(swappableLadders[prevLadderIndex].Prefab, swappableLadders[currentLadderIndex].VerticalPosition);
            tmpLadders[nextLadderIndex] = new(swappableLadders[currentLadderIndex].Prefab, swappableLadders[nextLadderIndex].VerticalPosition);
        }
        swappableLadders = tmpLadders;
    }


    /**
     * Changes player object's parent.
     * 
     * @param void
     */
    private void RepositionPlayer()
    {
        Player.transform.SetParent(swappableLadders[currentLadderIndex].Prefab.transform, true);
    }

    /**
     * Returns a random point in a sphere of given radius around the player.
     * 
     * @param float
     *   Sphere radius.
     *   
     * @return Vector3
     *   Point coordinates vector.
     *   
     */
    public Vector3 GetRandomPointNearPlayer(float radius)
    {
        return GameManager.Manager.Player.transform.position + Random.insideUnitSphere * radius;
    }

    /**
     * Returns random boolean.
     * 
     * @return bool
     */
    public bool GetRandomBoolean()
    {
        return Random.value > 0.5f;
    }

}
