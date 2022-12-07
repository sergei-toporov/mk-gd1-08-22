using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnableBase : MonoBehaviour
{
    [SerializeField] protected CharactersStatsBase stats;
    public CharactersStatsBase Stats { get => stats; }

    [SerializeField] protected HealthBar healthBarPrefab;
    public HealthBar HealthBarPrefab { get => healthBarPrefab; }
    
    protected HealthBar healthBar;
    public HealthBar HealthBar { get => healthBar; }

    protected Canvas spawnableCanvas;

    protected void Awake()
    {
        if (!InitialCheck())
        {
            Debug.LogError("Errors occurred during the initial check! Please, fix them to proceed.");
            Application.Quit();
        }

        spawnableCanvas = GetComponentInChildren<Canvas>();

        if (healthBarPrefab != null) {            
            float meshHeight = GetComponent<MeshFilter>().sharedMesh.bounds.size.y;
            Vector3 position = transform.position + new Vector3(.0f, meshHeight, .0f);
            healthBar = Instantiate(healthBarPrefab, position, Quaternion.identity);
            healthBar.gameObject.transform.SetParent(spawnableCanvas.transform);
        }
        else
        {
            spawnableCanvas.gameObject.SetActive(false);
        }

        stats.Health = stats.HealthMaxBase;
        stats.Mana = stats.ManaMaxBase;
        stats.MovementSpeed = stats.MovementSpeedBase;
        stats.AttackSpeed = stats.AttackSpeedBase;
    }

    protected bool InitialCheck()
    {
        if (stats == null)
        {
            Debug.LogError($"The stats asset hasn't been set on '{gameObject.name}'. Can't proceed without it!");
            return false;
        }

        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas == null)
        {
            Debug.LogError($"The canvas component is not found in any children. It is required to show UI elements for the '{gameObject.name}'");
            return false;
        }
        return true;
    }
}
