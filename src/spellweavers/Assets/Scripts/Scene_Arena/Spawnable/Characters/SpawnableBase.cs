using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public struct CharacterStats
{
    public float healthBase;
    public float health;
    public float manaBase;
    public float mana;
    public float movementSpeedBase;
    public float movementSpeed;
    public float attackSpeedBase;
    public float attackSpeed;
    public float healthRegenBase;
    public float healthRegen;
    public float manaRegenBase;
    public float manaRegen;
}

public class SpawnableBase : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBarPrefab;
    public HealthBar HealthBarPrefab { get => healthBarPrefab; }
    
    protected HealthBar healthBar;
    public HealthBar HealthBar { get => healthBar; }

    protected Canvas spawnableCanvas;

    protected CharacterStats charStats;
    public CharacterStats CharStats { get => charStats; }

    [SerializeField] protected CharacterClassMetadata baseStats;
    public CharacterClassMetadata BaseStats { get => baseStats; }

    protected virtual void Awake()
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

        SetStartStats();
    }

    protected bool InitialCheck()
    {
        if (baseStats.baseHealth == 0)
        {
            Debug.LogError($"Wrong base stats asset set on '{gameObject.name}'. Fix it pls!");
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

    protected void SetStartStats()
    {
        charStats.health = baseStats.baseHealth;
        charStats.healthBase = baseStats.baseHealth;
        charStats.mana = baseStats.baseMana;
        charStats.manaBase = baseStats.baseMana;
        charStats.movementSpeed = baseStats.baseMovementSpeed;
        charStats.movementSpeedBase = baseStats.baseMovementSpeed;
        charStats.attackSpeed = baseStats.baseAttackSpeed;
        charStats.attackSpeedBase = baseStats.baseAttackSpeed;
        charStats.manaRegen = baseStats.baseManaRegeneration;
        charStats.manaRegenBase = baseStats.baseManaRegeneration;
        charStats.healthRegen = baseStats.baseHealthRegeneration;
        charStats.healthRegenBase = baseStats.baseHealthRegeneration;
    }

    public void TakeDamage() {
        charStats.health -= 5.0f;
        if (healthBar.gameObject.scene.rootCount != 0)
        {
            healthBar.BarValueChange.Invoke();
        }
        Debug.Log($"Damage taken by {name}. Health is now: {charStats.health}");
        Debug.Log($"HPb inv: {healthBar.BarValueChange}");

    }

    public void AddBaseStats(CharacterClassMetadata metadata)
    {
        baseStats = metadata;
    }
}
