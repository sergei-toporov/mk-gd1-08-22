using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnablePlayer : SpawnableBase
{
    protected PlayerController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }
    public void RecalculateStats()
    {
        float diff = 0.0f;
        foreach (PlayerFeat feat in controller.Feats.Values)
        {
            switch (feat.affectedStat)
            {
                case AffectedStat.HealthBase:
                    diff = BaseStats.baseHealth + BaseStats.baseHealth / 100 * feat.deltaPercent * feat.currentLevel - charStats.healthBase;
                    charStats.healthBase += diff;
                    charStats.health += diff;
                    break;
                case AffectedStat.ManaBase:
                    diff = BaseStats.baseMana + BaseStats.baseMana / 100 * feat.deltaPercent * feat.currentLevel - charStats.manaBase;
                    charStats.manaBase += diff;
                    charStats.mana += diff;
                    break;
                case AffectedStat.MovementSpeedBase:
                    diff = BaseStats.baseMovementSpeed + BaseStats.baseMovementSpeed / 100 * feat.deltaPercent * feat.currentLevel - charStats.movementSpeedBase;
                    charStats.movementSpeedBase += diff;
                    charStats.movementSpeed += diff;
                    break;
                case AffectedStat.AttackRangeBase:
                    diff = BaseStats.baseAttackRange + BaseStats.baseAttackRange / 100 * feat.deltaPercent * feat.currentLevel - charStats.attackRangeBase;
                    charStats.attackRangeBase += diff;
                    charStats.attackRange += diff;
                    break;
                case AffectedStat.AttacksPerMinuteBase:
                    diff = BaseStats.baseAttacksPerMinute + BaseStats.baseAttacksPerMinute / 100 * feat.deltaPercent * feat.currentLevel - charStats.attacksPerMinuteBase;
                    charStats.attacksPerMinuteBase += diff;
                    charStats.attacksPerMinute += diff;
                    break;
                case AffectedStat.DamageBase:
                    diff = BaseStats.baseDamage + BaseStats.baseDamage / 100 * feat.deltaPercent * feat.currentLevel - charStats.damageBase;
                    charStats.damageBase += diff;
                    charStats.damage += diff;
                    break;
                case AffectedStat.DamageRadiusBase:
                    diff = BaseStats.baseDamageRadius + BaseStats.baseDamageRadius / 100 * feat.deltaPercent * feat.currentLevel - charStats.damageRadiusBase;
                    charStats.damageRadiusBase += diff;
                    charStats.damageRadius += diff;
                    break;
                case AffectedStat.HealthRegenBase:
                    diff = BaseStats.baseHealthRegeneration + BaseStats.baseHealthRegeneration / 100 * feat.deltaPercent * feat.currentLevel - charStats.healthRegenBase;
                    charStats.healthRegenBase += diff;
                    charStats.healthRegen += diff;
                    break;
                case AffectedStat.ManaRegenBase:
                    diff = BaseStats.baseManaRegeneration + BaseStats.baseManaRegeneration / 100 * feat.deltaPercent * feat.currentLevel - charStats.manaRegenBase;
                    charStats.manaRegenBase += diff;
                    charStats.manaRegen += diff;
                    break;
            }            
        }

        UpdateBars();
    }
}
