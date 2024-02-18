using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItemSwitchInteraction : MonoBehaviour
{
    [SerializeField]
    InventoryItemSwitch itemSwitch;

    [SerializeField]
    int keyIndex = 0;
    [SerializeField]
    public InventoryKey selectedKey;

    [SerializeField]
    Transform uiAnchor;

    public InventoryKeySetEvent OnKeySet;

    List<InventoryKey> validInventoryKeys;
    public void OnInteractionStarted()
    {
        UIInventoryItemSwitchDisplay.Instance.ShowDisplayAt(uiAnchor);

        if (itemSwitch.AreRequirementsMet(out var current, out var total, out var isMisc) == false)
        {
            UIInventoryItemSwitchDisplay.Instance.ShowLockedWindow(current, total, isMisc);
            return;
        }

        selectedKey = itemSwitch.activeKey;
        ClearKey();


        validInventoryKeys = itemSwitch.GetValidInventoryKeys();
        UIInventoryItemSwitchDisplay.Instance.ShowInventory(validInventoryKeys);
        UIInventoryItemSwitchDisplay.Instance.SetSelectedKey(selectedKey);

        itemSwitch.ShowKey(selectedKey);
        OnKeySet?.Invoke(selectedKey);
        validInventoryKeys.Contains(selectedKey, out keyIndex);
        enabled = true;
    }

    public void OnInteractionEnded()
    {
        SubmitKey();
        itemSwitch.ShowKey(itemSwitch.activeKey);
        UIInventoryItemSwitchDisplay.Instance.HideDisplay();
        enabled = false;
    }

    void Update()
    {
        var delta = Input.mouseScrollDelta.y;
        delta = delta + (Input.GetKeyDown(KeyCode.Q) ? -1f : Input.GetKeyDown(KeyCode.E) ? 1f : 0f);
        if (Mathf.Approximately(delta, 0))
        {
            return;
        }

        if (delta < 0)
        {
            keyIndex++;
            if (keyIndex >= validInventoryKeys.Count)
            {
                keyIndex = 0;
            }
        }
        else
        {
            keyIndex--;
            if (keyIndex < 0)
            {
                keyIndex = validInventoryKeys.Count - 1;
            }
        }

        selectedKey = validInventoryKeys[keyIndex];
        OnKeySet?.Invoke(selectedKey);
        itemSwitch.ShowKey(selectedKey);
        UIInventoryItemSwitchDisplay.Instance.SetSelectedKey(selectedKey);
    }

    private void SubmitKey()
    {
        Player.Instance.UseKey(selectedKey);
        itemSwitch.SetKey(selectedKey);
        UIInventoryItemSwitchDisplay.Instance.SetSelectedKey(selectedKey);
    }

    private void ClearKey()
    {
        var oldKey = itemSwitch.RemoveKey();
        if (oldKey != InventoryKey.None)
        {
            Player.Instance.AddKey(oldKey);
        }
        UIInventoryItemSwitchDisplay.Instance.SetSelectedKey(InventoryKey.None);
    }
}


[System.Serializable]
public class InventoryKeySetEvent : UnityEvent<InventoryKey> { }