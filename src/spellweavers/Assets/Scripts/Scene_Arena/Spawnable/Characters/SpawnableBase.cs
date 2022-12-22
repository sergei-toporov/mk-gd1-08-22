using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MonsterDifficultyLevels
{
    Easy,
    Medium,
    Hard,
    Boss
}

[Serializable]
public struct CharacterStats
{
    public float healthBase;
    public float health;
    public float manaBase;
    public float mana;
    public float movementSpeedBase;
    public float movementSpeed;
    public float attacksPerMinuteBase;
    public float attacksPerMinute;
    public float attackRangeBase;
    public float attackRange;
    public float damageBase;
    public float damage;
    public float damageRadiusBase;
    public float damageRadius;
    public float healthRegenBase;
    public float healthRegen;
    public float manaRegenBase;
    public float manaRegen;
}

abstract public class SpawnableBase : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBarPrefab;
    public HealthBar HealthBarPrefab { get => healthBarPrefab; }
    
    protected HealthBar healthBar;
    public HealthBar HealthBar { get => healthBar; }

    protected Canvas spawnableCanvas;

    [SerializeField] protected CharacterStats charStats;
    public CharacterStats CharStats { get => charStats; }

    [SerializeField] protected CharacterClassMetadata baseStats;
    public CharacterClassMetadata BaseStats { get => baseStats; }

    protected string className;
    public string ClassName { get => className; }
    protected WeaponHitter hitterPrefab;
    public WeaponHitter HitterPrefab { get => hitterPrefab; }


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
        className = baseStats.defaultName;
        hitterPrefab = baseStats.weaponHitterPrefab;
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
        charStats.attacksPerMinute = baseStats.baseAttacksPerMinute;
        charStats.attacksPerMinuteBase = baseStats.baseAttacksPerMinute;
        charStats.attackRange = baseStats.baseAttackRange;
        charStats.attackRangeBase = baseStats.baseAttackRange;
        charStats.manaRegen = baseStats.baseManaRegeneration;
        charStats.manaRegenBase = baseStats.baseManaRegeneration;
        charStats.healthRegen = baseStats.baseHealthRegeneration;
        charStats.healthRegenBase = baseStats.baseHealthRegeneration;
        charStats.damage = baseStats.baseDamage;
        charStats.damageBase = baseStats.baseDamage;
        charStats.damageRadius = baseStats.baseDamageRadius;
        charStats.damageRadiusBase = baseStats.baseDamageRadius;

    }

    public void TakeDamage(SpawnableBase foe) {
        charStats.health -= foe.CharStats.damage;
        if (charStats.health <= 0)
        {
            CharacterDeath();
        }

        if (healthBar.gameObject.scene.rootCount != 0)
        {
            healthBar.BarValueChange.Invoke();
        }
    }

    public void AddBaseStats(CharacterClassMetadata metadata)
    {
        baseStats = metadata;
    }

    protected void UpdateBars()
    {
        if (healthBar.gameObject.scene.rootCount != 0)
        {
            healthBar.ResetValues();
        }
    }

    abstract protected void CharacterDeath();
}
