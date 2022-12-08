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

    protected SpawnablePlayer playerStats;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        if (TryGetComponent(out SpawnablePlayer stats)) {
            playerStats = stats;
        }
        Debug.Log($"Player stats (Controller): {playerStats.Stats.Health} / {playerStats.Stats.HealthMaxBase}");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;
        Vector3 movementDirection = new Vector3(x, 0.0f, z);

        PlayerMover.SimpleMove(playerStats.Stats.MovementSpeedBase * movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }
    }

    protected void TakeDamage()
    {
        
        playerStats.Stats.Health -= 5.0f;
        playerStats.HealthBar.BarValueChange.Invoke();
        Debug.Log($"Damage taken. Health is now: {playerStats.Stats.Health}");
        Debug.Log($"HPb inv: {playerStats.HealthBar.BarValueChange}");
    }
}
