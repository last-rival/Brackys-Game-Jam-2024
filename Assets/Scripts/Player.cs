using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourInstance<Player>
{
    [SerializeField]
    Transform carryAnchor;

    [SerializeField]
    int inventoryLayer;

    Dictionary<InventoryKey, int> inventory = new Dictionary<InventoryKey, int>();

    Transform carryItem;
    private int carryItemLayer;

    public void Carry(Transform carryItem)
    {
        this.carryItem = carryItem;
        carryItemLayer = carryItem.gameObject.layer;

        Vector3 localScale = carryItem.transform.localScale;
        carryItem.SetParent(carryAnchor, true);
        carryItem.transform.localPosition = Vector3.zero;
        carryItem.transform.localScale = localScale;
        carryItem.gameObject.SetLayerRecursive(inventoryLayer);

        carryItem.GetComponent<ICarryItem>()?.OnCarry(transform);
    }

    public void Drop()
    {
        if (carryItem == null)
        {
            return;
        }

        Vector3 localScale = carryItem.transform.localScale;
        carryItem.SetParent(null);
        carryItem.transform.localScale = localScale;
        carryItem.gameObject.SetLayerRecursive(carryItemLayer);

        carryItem.GetComponent<ICarryItem>()?.OnDrop(transform);
        carryItem = null;
    }

    public bool HasKey(InventoryKey key, out int count)
    {
        if (inventory.TryGetValue(key, out count))
        {
            return count > 0;
        }

        return false;
    }

    public void UseKey(InventoryKey key, int amount = 1)
    {
        if (inventory.TryGetValue(key, out var count))
        {
            inventory[key] = Mathf.Max(count - amount, 0);
        }
    }

    public void AddKey(InventoryKey key, int amount = 1)
    {
        if (inventory.TryGetValue(key, out var count))
        {
            amount += count;
        }

        inventory[key] = amount;
        UI_Manager.Instance.UpdateInventory(inventory);
    }
}
