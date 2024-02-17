using UnityEngine;

public class InventoryItemSwitchInteraction : MonoBehaviour
{
    [SerializeField]
    InventoryItemSwitch itemSwitch;

    [SerializeField]
    int keyIndex = 0;
    [SerializeField]
    InventoryKey selectedKey;
    public void OnInteractionStarted()
    {
        if (Player.Instance.inventory.Count == 0 && itemSwitch.activeKey == InventoryKey.None)
            return;

        itemSwitch.keyChain.ShowKey(itemSwitch.activeKey);
        enabled = true;
        keyIndex = 0;
    }

    public void OnInteractionEnded()
    {
        itemSwitch.keyChain.ShowKey(itemSwitch.activeKey);
        enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SubmitKey();
            return;
        }

        if (Input.GetMouseButton(1))
        {
            ClearKey();
            return;
        }

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

        if (delta > 0)
        {
            keyIndex++;
            if (keyIndex >= inventory.Count)
            {
                keyIndex = 0;
            }
        }
        else
        {
            keyIndex--;
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

        itemSwitch.keyChain.ShowKey(selectedKey);
    }

    private void SubmitKey()
    {
        ClearKey();
        Player.Instance.UseKey(selectedKey);
        itemSwitch.SetKey(selectedKey);
    }

    private void ClearKey()
    {
        var oldKey = itemSwitch.RemoveKey();
        if (oldKey != InventoryKey.None)
        {
            Player.Instance.AddKey(oldKey);
        }
    }
}
