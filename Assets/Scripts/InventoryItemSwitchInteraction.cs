using UnityEngine;
using UnityEngine.Events;

public class InventoryItemSwitchInteraction : MonoBehaviour
{
    [SerializeField]
    InventoryItemSwitch itemSwitch;

    [SerializeField]
    int keyIndex = 0;
    [SerializeField]
    InventoryKey selectedKey;

    [SerializeField]
    Transform uiAnchor;

    public InventoryKeySetEvent OnKeySet;

    public void OnInteractionStarted()
    {
        selectedKey = itemSwitch.activeKey;
        ClearKey();

        UIInventoryItemSwitchDisplay.Instance.ShowDisplayAt(uiAnchor);
        UIInventoryItemSwitchDisplay.Instance.ShowInventory();
        UIInventoryItemSwitchDisplay.Instance.SetSelectedKey(selectedKey);
        itemSwitch.keyChain.ShowKey(selectedKey);
        OnKeySet?.Invoke(selectedKey);

        enabled = true;
        if (Player.Instance.GetIndexOfKey(selectedKey, out keyIndex) == false)
        {
            keyIndex = -1;
        }
    }

    public void OnInteractionEnded()
    {
        SubmitKey();
        itemSwitch.keyChain.ShowKey(itemSwitch.activeKey);
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

        var inventory = Player.Instance.inventory;
        if (inventory.Count == 0)
        {
            return;
        }

        if (delta < 0)
        {
            keyIndex++;
            if (keyIndex == inventory.Count)
            {
                selectedKey = InventoryKey.None;
                OnKeySet?.Invoke(selectedKey);
                ClearKey();
                return;
            }

            if (keyIndex > inventory.Count)
            {
                keyIndex = 0;
            }
        }
        else
        {
            keyIndex--;
            if (keyIndex == -1)
            {
                selectedKey = InventoryKey.None;
                OnKeySet?.Invoke(selectedKey);
                ClearKey();
                return;
            }

            if (keyIndex < 0)
            {
                keyIndex = inventory.Count - 1;
            }
        }


        int index = 0;
        selectedKey = itemSwitch.activeKey;
        foreach (var item in inventory)
        {
            if (index == keyIndex)
            {
                selectedKey = item.Key;
                break;
            }
            index++;
        }

        OnKeySet?.Invoke(selectedKey);
        itemSwitch.keyChain.ShowKey(selectedKey);
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