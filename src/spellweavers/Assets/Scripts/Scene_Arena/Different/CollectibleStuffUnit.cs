using UnityEngine;

public class CollectibleStuffUnit : MonoBehaviour
{
    protected CollectibleStuff unitData;
    public CollectibleStuff UnitData { get => unitData; }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out SpawnablePlayer player))
        {
            player.StuffPickup(this);
            Destroy(gameObject);
        }
    }

    public void SetParameters(CollectibleStuff providedData)
    {
        unitData = providedData;
    }
}
