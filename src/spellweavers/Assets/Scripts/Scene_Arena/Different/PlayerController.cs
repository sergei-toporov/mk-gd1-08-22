using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected Joystick joystick;
    public Joystick Joystick { get => joystick; }

    protected CharacterController playerMover;
    public CharacterController PlayerMover { get => playerMover ?? GetComponent<CharacterController>(); }

    protected SpawnablePlayer playerObject;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        playerObject = gameObject.GetComponent<SpawnablePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;
        Vector3 movementDirection = new Vector3(x, 0.0f, z);

        PlayerMover.SimpleMove(playerObject.CharStats.movementSpeedBase * movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }
    }

    protected void TakeDamage()
    {
        playerObject.TakeDamage();
    }
}
