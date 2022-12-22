using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitter : MonoBehaviour
{
    // Start is called before the first frame update
    protected Vector3 startPosition;

    protected float ttl = 5.0f;
    protected WaitForSeconds ttlObject;
    protected SpawnableBase parent;
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(LifeExpirationCoroutine());
        ttlObject = new WaitForSeconds(ttl);
    }

    public void SetParent(SpawnableBase providedObject)
    {
        parent = providedObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (parent != null && collision.gameObject.TryGetComponent(out SpawnableMonster monster))
        {
            monster.TakeDamage(parent);            
        }
        DestroyObject();
    }

    protected IEnumerator LifeExpirationCoroutine()
    {
        yield return new WaitForSeconds(ttl);
        DestroyObject();
    }

    protected void FixedUpdate()
    {
        if (parent != null)
        {
            if (Vector3.Distance(startPosition, transform.position) >= parent.CharStats.attackRange)
            {
                DestroyObject();
            }
        }
    }

    protected void DestroyObject()
    {
        Destroy(gameObject);
    }
}
