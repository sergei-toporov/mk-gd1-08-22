using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableMonster : SpawnableBase
{
    protected CharacterController controller;
    public CharacterController Controller { get => controller ?? GetComponent<CharacterController>(); }

    protected bool isFighting = false;

    protected void Update()
    {
        Vector3 lookAt = new Vector3(
            ArenaManager.Manager.Player.transform.position.x,
            0.0f,
            ArenaManager.Manager.Player.transform.position.z
            );
        //transform.LookAt(ArenaManager.Manager.Player.transform);
        transform.LookAt(lookAt);

        Vector3 dir = (ArenaManager.Manager.Player.transform.position - transform.position).normalized;
        dir.y = -9.81f;

        Controller.SimpleMove(charStats.movementSpeed * dir);
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController Player))
        {
            Debug.Log("CollisionEnter");
            if (!isFighting)
            {
                isFighting = true;
                StartCoroutine(TakingDamage());
            }
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController Player))
        {
            Debug.Log("CollisionExit");
            if (isFighting)
            {
                isFighting = false;
                StopCoroutine(TakingDamage());
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController Player))
        {
            Debug.Log("TriggerEnter");
            if (!isFighting)
            {
                isFighting = true;
                StartCoroutine(TakingDamage());
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController Player))
        {
            Debug.Log("TriggerExit");
            if (isFighting)
            {
                isFighting = false;
                StopCoroutine(TakingDamage());

            }
        }
    }



    protected IEnumerator TakingDamage()
    {
        while (isFighting)
        {
            charStats.health -= 5.0f;
            if (charStats.health <= 0.0f)
            {
                Destroy(gameObject);
            }
            
            healthBar.BarValueChange.Invoke();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
