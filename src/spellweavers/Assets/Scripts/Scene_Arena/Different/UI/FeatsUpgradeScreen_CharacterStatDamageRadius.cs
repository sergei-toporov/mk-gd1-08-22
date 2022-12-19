using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatsUpgradeScreen_CharacterStatDamageRadius : FeatsUpgradeScreen_CharacterStatValueBase
{
    public override void UpdateText()
    {
        base.UpdateText();
        text.text = $"{ArenaManager.Manager.PlayerBase.CharStats.damageRadiusBase}";
    }
}
