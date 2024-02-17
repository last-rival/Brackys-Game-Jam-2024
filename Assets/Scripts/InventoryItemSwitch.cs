using UnityEngine;

public class InventoryItemSwitch : MonoBehaviour
{
    [SerializeField]
    public InventoryKey[] requiredKeys;
    [SerializeField]
    public InventoryKey[] activeKeys;

    public WorldButtonClickEvent OnSwithUpdated;

    public bool AddKey(InventoryKey key)
    {
        for (int i = 0; i < activeKeys.Length; i++)
        {
            if (activeKeys[i] == InventoryKey.None)
            {
                activeKeys[i] = key;
                OnSwithUpdated?.Invoke(IsUnlocked());
                return true;
            }
        }

        return false;
    }

    public bool RemoveKey(InventoryKey key)
    {
        if (activeKeys.Contains(key, out var index))
        {
            activeKeys[index] = InventoryKey.None;
            OnSwithUpdated?.Invoke(IsUnlocked());
            return true;
        }

        return false;
    }

    public bool IsUnlocked()
    {
        for (int i = 0; i < requiredKeys.Length; i++)
        {
            if (activeKeys.Contains(requiredKeys[i], out _) == false)
                return false;
        }

        return true;
    }
}
