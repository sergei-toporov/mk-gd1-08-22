using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatsUpgradeScreen_CharacterStatsPaneController : MonoBehaviour
{
    [SerializeField] protected FeatsUpgradeScreen_CharacterStatsPaneController controller;
    public FeatsUpgradeScreen_CharacterStatsPaneController Controller { get => controller ?? GetComponent<FeatsUpgradeScreen_CharacterStatsPaneController>(); }

    protected List<FeatsUpgradeScreen_CharacterStatValueBase> stats = new List<FeatsUpgradeScreen_CharacterStatValueBase>();

    protected void OnEnable()
    {
        if (stats.Count == 0)
        {
            foreach (FeatsUpgradeScreen_CharacterStatValueBase stat in GetComponentsInChildren<FeatsUpgradeScreen_CharacterStatValueBase>())
            {
                stats.Add(stat);
            }
        }
        UpdateStats();
    }

    public void UpdateStats()
    {
        foreach (FeatsUpgradeScreen_CharacterStatValueBase stat in stats)
        {
            stat.UpdateText();
        }
    }
}
