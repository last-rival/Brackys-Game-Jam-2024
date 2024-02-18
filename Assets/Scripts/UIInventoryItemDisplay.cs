using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryItemDisplay : MonoBehaviour
{
    [SerializeField]
    UIInventoryIcon[] inventoryIcons;

    [SerializeField]
    InventoryKeyData itemData;

    public void ShowInventoryIcon(InventoryKey key, int amount)
    {
        return;
        if (itemData.TryGetDataForKey(key, out var data) == false)
        {
            return;
        }

        for (int i = 0; i < inventoryIcons.Length; i++)
        {
            var itemIcon = inventoryIcons[i];
            if (itemIcon.key == key || itemIcon.gameObject.activeSelf == false)
            {
                itemIcon.SetInventoryIcon(data, amount);
                itemIcon.gameObject.SetActive(amount > 0);
                break;
            }
        }
    }

    public void ClearItems()
    {
        return;
        foreach (var icon in inventoryIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
