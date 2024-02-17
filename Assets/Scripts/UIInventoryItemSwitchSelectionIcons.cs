using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItemSwitchSelectionIcons : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    GameObject selected;

    public InventoryKey key;

    public void SetImage(InventoryItemData data, InventoryKey key)
    {
        this.key = key;
        icon.sprite = data.image;
        icon.color = data.color;
        icon.SetAllDirty();
    }

    public void SetSelected(bool isSelected)
    {
        selected.SetActive(isSelected);
    }
}
