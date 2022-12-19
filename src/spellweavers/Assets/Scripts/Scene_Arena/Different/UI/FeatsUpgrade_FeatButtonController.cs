using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeatsUpgrade_FeatButtonController : MonoBehaviour
{
    protected FeatsUpgrade_FeatButtonController controller;
    public FeatsUpgrade_FeatButtonController Controller { get => controller ?? GetComponent<FeatsUpgrade_FeatButtonController>(); }

    protected FeatsUpgradeScreen_CharacterStatsPaneController charStatsPane;

    protected Button button;

    protected TextMeshProUGUI featName;
    protected TextMeshProUGUI featCurrentLevelValue;
    protected TextMeshProUGUI featUpgradeCostValue;

    protected PlayerFeat feat;
    protected string featKey;

    protected void OnEnable()
    {
        button = GetComponent<Button>();
        featName = GetComponentInChildren<FeatButton_FeatName>().GetComponent<TextMeshProUGUI>();
        featCurrentLevelValue = GetComponentInChildren<FeatButton_CurrentLevelValue>().GetComponent<TextMeshProUGUI>();
        featUpgradeCostValue = GetComponentInChildren<FeatButton_NextLevelCostValue>().GetComponent<TextMeshProUGUI>();
        button.onClick.AddListener(OnClickListener);
        charStatsPane = FindObjectOfType<FeatsUpgradeScreen_CharacterStatsPaneController>();        
    }

    protected void Update()
    {
        /**
         * Find out why it scaled way too much
         */
        if (transform.localScale.x < 0.5f)
        {
            transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
        }
    }

    public void AssignFeat(string key)
    {
        featKey = key;
        ProcessFeatData();
    }

    protected void ProcessFeatData()
    {
        feat = ArenaManager.Manager.Player.GetPlayerFeat(featKey);

        featName.text = feat.name;
        featCurrentLevelValue.text = $"{feat.currentLevel}";
        featUpgradeCostValue.text = $"{feat.currentImprovementCost}";
    }

    protected void OnClickListener()
    {
        ArenaManager.Manager.Player.AddFeat(featKey);
        ProcessFeatData();
        charStatsPane.UpdateStats();
    }

   
}
