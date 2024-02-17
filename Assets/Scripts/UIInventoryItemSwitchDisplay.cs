using DG.Tweening;
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

    public void ShowInventory()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].key = InventoryKey.None;
            icons[i].SetSelected(false);
            icons[i].gameObject.SetActive(false);
        }

        int index = 0;
        var inventory = Player.Instance.inventory;
        foreach (var item in inventory)
        {
            if (inventoyData.TryGetDataForKey(item.Key, out var itemData))
            {
                if (item.Value < 1)
                {
                    continue;
                }

                icons[index].gameObject.SetActive(true);
                icons[index].SetImage(itemData, item.Key);
                index++;
            }
        }

        icons[index].gameObject.SetActive(true);
        inventoyData.TryGetDataForKey(InventoryKey.None, out var noneData);
        icons[index].SetImage(noneData, InventoryKey.None);
    }

    public void SetSelectedKey(InventoryKey key)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].SetSelected(icons[i].key == key);
        }
    }
}

