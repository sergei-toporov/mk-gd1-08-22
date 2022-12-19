using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatsUpgradeScreen_CharacterStatMP : FeatsUpgradeScreen_CharacterStatValueBase
{
    public override void UpdateText()
    {
        base.UpdateText();
        text.text = $"{ArenaManager.Manager.PlayerBase.CharStats.mana} / {ArenaManager.Manager.PlayerBase.CharStats.manaBase}";
    }
}
