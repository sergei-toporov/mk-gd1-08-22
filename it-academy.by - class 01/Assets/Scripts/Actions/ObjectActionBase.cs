using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Expertimental class. Serves as the base for other actions.
 */
public class ObjectActionBase : MonoBehaviour
{
    /**
     * X-axis limit for spawned objects.
     * 
     * @param List<float>
     * */
    [SerializeField] protected List<float> xBorder = new List<float> { -20.0f, 20.0f };

    /**
     * Y-axis limit for spawned objects.
     * 
     * @param List<float>
     * */
    [SerializeField] protected List<float> yBorder = new List<float> { -10.0f, 10.0f };

    /**
     * Z-axis limit for spawned objects.
     * 
     * @param List<float>
     * */
    [SerializeField] protected List<float> zBorder = new List<float> { -20.0f, 20.0f };

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
    void Start()
    {
        
    }

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
