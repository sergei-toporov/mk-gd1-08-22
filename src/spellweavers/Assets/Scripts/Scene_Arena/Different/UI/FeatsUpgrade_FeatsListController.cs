using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatsUpgrade_FeatsListController : MonoBehaviour
{
    [SerializeField] protected FeatsUpgrade_FeatsListController controller;
    public FeatsUpgrade_FeatsListController Controller { get => controller ?? GetComponent<FeatsUpgrade_FeatsListController>(); }

    [SerializeField] protected FeatsUpgrade_FeatButtonController buttonPrefab;
    public FeatsUpgrade_FeatButtonController ButtonPrefab { get => buttonPrefab; }

    [SerializeField] protected FeatsListContent buttonsBlock;
    public FeatsListContent ButtonsBlock { get => buttonsBlock; }

    protected void Awake()
    {
        foreach (string featKey in ArenaManager.Manager.AvailablePlayerFeats.Keys)
        {
            FeatsUpgrade_FeatButtonController button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(buttonsBlock.transform);
            button.AssignFeat(featKey);
        }
    }
}
