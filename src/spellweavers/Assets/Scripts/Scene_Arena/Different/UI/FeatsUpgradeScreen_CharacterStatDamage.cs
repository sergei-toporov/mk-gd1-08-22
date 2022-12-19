using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatsUpgradeScreen_CharacterStatDamage : FeatsUpgradeScreen_CharacterStatValueBase
{
    public override void UpdateText()
    {
        base.UpdateText();
        text.text = $"{ArenaManager.Manager.PlayerBase.CharStats.damageBase}";
    }
}
