using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviourInstance<UI_Manager>
{
    [SerializeField]
    UIInventoryItemDisplay inventoryDisplay;

    [SerializeField]
    UIIntreactionButtonDisplay interactionDisplay;

    public void ShowInteractionButton(KeyCode key, Transform worldAnchor, bool show)
    {
        interactionDisplay.ShowInteractionButton(key, worldAnchor, show);
    }

    internal void UpdateInventroy(Dictionary<InventoryKey, int> inventory)
    {
        foreach (var item in inventory)
        {
            inventoryDisplay.ShowInventoryIcon(item.Key, item.Value);
        }
    }
}
