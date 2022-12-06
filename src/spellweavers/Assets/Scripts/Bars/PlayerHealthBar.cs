using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : HealthBar
{
    protected SpawnablePlayer stats;
    protected override void Awake()
    {
        base.Awake();
        if (TryGetComponent(out SpawnablePlayer playerStats))
        {
            barValueChange = UpdateBar;
            stats = playerStats;
        }
    }

    protected override void UpdateBar()
    {
        slider.value = stats.Stats.Health / stats.Stats.HealthMaxBase;
    }
}
