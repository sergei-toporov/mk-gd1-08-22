using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUI_OpenFeatsUpgradeScreenButton : MonoBehaviour
{
    protected Button button;

    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickListener);
    }

    protected void OnClickListener()
    {
        ArenaWorkflowManager.Manager.SwitchState(ArenaStates.FeatsUpgradeScreen);
    }
}
