using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryItemSwitchDisplay : MonoBehaviourInstance<UIInventoryItemSwitchDisplay>
{
    [SerializeField]
    GameObjectBillboard billboard;

    [SerializeField]
    InventoryKeyData inventoyData;

    [SerializeField]
    UIInventoryItemSwitchSelectionIcons[] icons;

    protected override void OnAwake()
    {
        gameObject.SetActive(false);
    }

    public void ShowDisplayAt(Transform anchor)
    {
        anchor.DOKill();

        billboard.enabled = true;

        transform.SetParent(anchor);
        transform.localPosition = Vector3.zero;
        anchor.localScale = Vector3.zero;
        anchor.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        gameObject.SetActive(true);
    }

    public void HideDisplay()
    {
        billboard.enabled = false;

        var anchor = transform.parent;
        anchor.DOKill();
        anchor.DOScale(Vector3.zero, 0.35f).SetEase(Ease.OutQuint).onComplete = DisableGO;
    }

    void DisableGO()
    {
        gameObject.SetActive(false);
        transform.parent.localScale = Vector3.one;
        transform.parent = null;
    }

    public void ShowInventory(List<InventoryKey> inventoryKeys)
    {
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
}

