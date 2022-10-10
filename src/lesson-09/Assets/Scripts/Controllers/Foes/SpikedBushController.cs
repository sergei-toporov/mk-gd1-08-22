using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Spiked bush controller
 */
public class SpikedBushController : FoesControllerBase
{
    /**
     * Amount of time the effect is lasting.
     * 
     * @param float
     */
    private float effectTime = 5.0f;

    /**
     * Force of the effect.
     * 
     * @param float
     */
    private float effectForce = 3.0f;

    /**
     * Causes player's slowering.
     *
     * @return void
     */
    protected override void TriggerAction()
    {
        StartCoroutine(GameManager.Manager.Player.SlowDownCharacterCoroutine(effectTime, effectForce));
    }
}
