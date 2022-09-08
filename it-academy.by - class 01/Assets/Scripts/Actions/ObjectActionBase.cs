using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Expertimental class. Serves as the base for other actions.
 */
public class ObjectActionBase : MonoBehaviour
{
    /**
     * Position borders: minimum.
     * 
     * @param Vector3
     */
    [SerializeField] protected Vector3 borderMin = new Vector3(-20.0f, 0.5f, -20.0f);

    /**
     * Position borders: maximum.
     * 
     * @param Vector3
     */
    [SerializeField] protected Vector3 borderMax = new Vector3(20.0f, 5.0f, 20.0f);

    /**
     * Timestamp of the last object activity.
     * 
     * @param float
     */ 
    [SerializeField] protected float timestamp = 0.0f;

    /**
     * Time gap between object's activities.
     * 
     * @param float
     */ 
    [SerializeField] protected float maxTimeDiff = 3.0f;

    /**
     * {inheritdoc}
     */ 
    void Update()
    {
        this.MakeAction();
    }

    /**
     * An action activation method.
     * 
     * Keeps activity logic of the particular action.
     * 
     * @return void
     */ 
    protected virtual void MakeAction()
    {

    }


}
