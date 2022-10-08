using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnableObjectsBase : MonoBehaviour
{
    /**
     * Speed range limits.
     * 
     * @param Vector2
     */
    [SerializeField] private Vector2 movementSpeedLimits = new(2.5f, 7.5f);

    /**
     * Object's speed.
     * 
     * @param float
     */
    [SerializeField] private float speed;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        speed = Random.Range(movementSpeedLimits.x, movementSpeedLimits.y);
    }


    /**
     * {@inheritdoc}
     */
    void Start()
    {
        
    }

    /**
     * {@inheritdoc}
     */
    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    /**
     * {@inheritdoc}
     */
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallsIdentifier Wall))
        {
            TriggerAction();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    /**
     * Object's specific action.
     * 
     * @return void
     */
    protected virtual void TriggerAction() { }
}
