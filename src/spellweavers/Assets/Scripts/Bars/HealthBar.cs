using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBar : StatBarBase
{
    protected SpawnableBase stats;
    protected void Start()
    {
        Debug.Log("PHPB Start");
        SpawnableBase spawnableStats = GetComponentInParent<SpawnableBase>();
        if (spawnableStats != null)
        {
            Debug.Log("PlayerHPBar init");
            barValueChange = UpdateBar;
            stats = spawnableStats;
            SetInitialValues(stats.Stats.HealthMaxBase, stats.Stats.HealthMaxBase);
        }
    }

    protected override void UpdateBar()
    {
        Debug.Log("Bar updated");
        slider.value = stats.Stats.Health;
    }
}
