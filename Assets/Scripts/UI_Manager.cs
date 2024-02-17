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

    public void ShowInteractionButton(InteractionKey key, Transform worldAnchor, bool show)
    {
        if (key == InteractionKey.None)
            return;

        interactionDisplay.ShowInteractionButton(key, worldAnchor, show);
    }

    internal void UpdateInventory(Dictionary<InventoryKey, int> inventory)
    {
        inventoryDisplay.ClearItems();

        foreach (var item in inventory)
        {
            inventoryDisplay.ShowInventoryIcon(item.Key, item.Value);
        }
    }
}
