using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItemSwitchDisplay : MonoBehaviourInstance<UIInventoryItemSwitchDisplay>
{
    [SerializeField]
    GameObjectBillboard billboard;

    [SerializeField]
    InventoryKeyData inventoyData;

    [SerializeField]
    UIInventoryItemSwitchSelectionIcons[] icons;

    [SerializeField]
    GameObject itemGrid;

    [SerializeField]
    UIInventorySwitchRequirementsDisplay requirementsDisplay;

    Vector3 localScale;
    Tween activeTween;

    protected override void OnAwake()
    {
        localScale = transform.localScale;
        gameObject.SetActive(false);
    }

    public void ShowDisplayAt(Transform anchor)
    {
        activeTween?.Kill();

        billboard.enabled = true;

        transform.SetParent(anchor);
        transform.localPosition = Vector3.zero;
        anchor.localScale = Vector3.zero;

        transform.localScale = localScale;
        activeTween = anchor.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        gameObject.SetActive(true);
    }

    public void HideDisplay()
    {
        billboard.enabled = false;
        activeTween?.Kill();

        var anchor = transform.parent;
        activeTween = anchor.DOScale(Vector3.zero, 0.35f).SetEase(Ease.OutQuint).OnComplete(DisableGO);
    }

    void DisableGO()
    {
        gameObject.SetActive(false);
        transform.parent.localScale = Vector3.one;
        transform.parent = null;
    }

    public void ShowInventory(List<InventoryKey> inventoryKeys)
    {
        itemGrid.SetActive(true);
        requirementsDisplay.gameObject.SetActive(false);

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].key = InventoryKey.None;
            icons[i].SetSelected(false);
            icons[i].gameObject.SetActive(false);
        }

        int index = 0;
        foreach (var key in inventoryKeys)
        {
            if (inventoyData.TryGetDataForKey(key, out var itemData))
            {
                icons[index].gameObject.SetActive(true);
                icons[index].SetImage(itemData, key);
                index++;
            }
        }
    }

    public void SetSelectedKey(InventoryKey key)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].SetSelected(icons[i].key == key);
        }
    }

    public void ShowLockedWindow(int current, int total, bool misc = false)
    {
        requirementsDisplay.gameObject.SetActive(true);
        itemGrid.SetActive(false);
        requirementsDisplay.ShowRequirements(current, total, misc);
    }
}

