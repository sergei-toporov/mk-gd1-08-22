using UnityEngine;

public class CollectibleResourceUnit : MonoBehaviour
{
    protected CollectibleResource unitData;
    public CollectibleResource UnitData { get => unitData; }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out SpawnablePlayer player))
        {
            player.ResourcePickup(this);
            Destroy(gameObject);
        }
    }

    public void SetParameters(CollectibleResource providedData)
    {
        unitData = providedData;
    }
}
