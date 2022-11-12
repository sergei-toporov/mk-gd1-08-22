using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private List<StaticPlateController> towerPlates;
    public List<StaticPlateController> TowerPlates { get => towerPlates; }

    [SerializeField] private bool plateMissed = false;
    public bool PlateMissed { get => plateMissed; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LowerTower()
    {
        transform.position -= new Vector3(0.0f, PlateGeneratorManager.Manager.MeshDimensions.y, 0.0f);
    }

    public void AddPlate()
    {
        if (PlateGeneratorManager.Manager.CheckPlatesOverlapping())
        {
            StaticPlateController staticPlate = PlateGeneratorManager.Manager.GenerateStaticPlate();
            staticPlate.transform.parent = transform;
            towerPlates.Add(staticPlate);
        }              
        else
        {
            PlateGeneratorManager.Manager.TransformActiveToRuined();
            plateMissed = true;
        }
    }
}
