using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableMonster : SpawnableBase
{
    protected CharacterController controller;
    public CharacterController Controller { get => controller ?? GetComponent<CharacterController>(); }

    protected void Update()
    {
        transform.LookAt(ArenaManager.Manager.Player.transform);

        Vector3 dir = (ArenaManager.Manager.Player.transform.position - transform.position).normalized;
        dir.y = -9.81f;
        Debug.Log($"target: {dir}");

        Controller.SimpleMove(5 * dir);
    }
}
