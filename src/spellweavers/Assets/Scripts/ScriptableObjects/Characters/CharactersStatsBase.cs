using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for creatures stats SO assets.
/// </summary>
public class CharactersStatsBase : ScriptableObject
{
    [SerializeField] protected float healthMaxBase;
    public float HealthMaxBase { get => healthMaxBase; }

    [SerializeField] protected float manaMaxBase;
    public float ManaMaxBase { get => manaMaxBase; }

    [SerializeField] protected float movementSpeedBase;
    public float MovementSpeedBase { get => movementSpeedBase; }

    [SerializeField] protected float attackSpeedBase;
    public float AttackSpeedBase { get => attackSpeedBase; }

    protected float health;
    public float Health { get => health; }

    protected float mana;
    public float Mana { get => mana; }

    protected float movementSpeed;
    public float MovementSpeed { get => movementSpeed; }

    protected float attackSpeed;
    public float AttackSpeed { get => attackSpeed; }


}
