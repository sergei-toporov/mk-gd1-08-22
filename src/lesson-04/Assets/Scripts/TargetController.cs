using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Target objects handler
 */
public class TargetController : MonoBehaviour
{
    /**
     * Initial target position.
     * 
     * @param Vector3
     */
    private Vector3 initialPosition;

    /**
     * Time till object repositioning.
     * 
     * @param float
     */
    private float positionRestoreTime = 15.0f;

    /**
     * Object's rigidbody.
     * 
     * @param Rigidbody
     */
    private Rigidbody objectRigidbody;

    /**
     * {@inheritdoc}
     */
    private void Awake()
    {
        initialPosition = transform.position;
        objectRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    /**
     * {@inheritdoc}
     */
    void Start()
    {
        StartCoroutine(SelfReposition());
    }

    /**
     * Restores object's position periodically.
     * 
     * @return IEnumerator
     */
    private IEnumerator SelfReposition()
    {
        while (true)
        {
            yield return new WaitForSeconds(positionRestoreTime);

            transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
            objectRigidbody.velocity = Vector3.zero;
        }        
    }
}
