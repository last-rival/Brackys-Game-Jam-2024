using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItemSwitch : MonoBehaviour
{
    //[SerializeField]
    //public InventoryKey[] requiredKeys;
    //[SerializeField]
    //public InventoryKey[] activeKeys;

    [Serializable]
    public struct InventoryKeyValuePair
    {
        public InventoryKey key;
        public int amount;
    }

    [SerializeField]
    public InventoryKey requiredKey;
    [SerializeField]
    public InventoryKey activeKey;

    [SerializeField]
    public InventoryKey[] validKeyOptions;

    [SerializeField]
    public InventoryKeyValuePair[] requiredItemInInventory;

    [SerializeField]
    KeyChain keyChain;

    public WorldButtonClickEvent OnSwitchUpdated;

    List<InventoryKey> keyOptions = new List<InventoryKey>(16);

    //void Start()
    //{
    //    activeKeys = new InventoryKey[requiredKeys.Length];
    //}

    //public bool AddKey(InventoryKey key)
    //{
    //    for (int i = 0; i < activeKeys.Length; i++)
    //    {
    //        if (activeKeys[i] == InventoryKey.None)
    //        {
    //            activeKeys[i] = key;
    //            OnKeysUpdated();
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    public void ShowKey(InventoryKey key)
    {
        OnSwitchUpdated?.Invoke(IsUnlockedByKey(key));
        keyChain.ShowKey(key);
    }

    public void SetKey(InventoryKey key)
    {
        activeKey = key;
        OnKeysUpdated();
    }

    public InventoryKey RemoveKey()
    {
        var key = activeKey;
        activeKey = InventoryKey.None;
        OnKeysUpdated();
        return key;
    }

    //public bool RemoveKey(InventoryKey key)
    //{
    //    if (activeKeys.Contains(key, out var index))
    //    {
    //        activeKeys[index] = InventoryKey.None;
    //        OnKeysUpdated();
    //        return true;
    //    }

    //    return false;
    //}

    //public void ClearActiveKeys()
    //{
    //    for (int i = 0; i < activeKeys.Length; i++)
    //    {
    //        activeKeys[i] = InventoryKey.None;
    //    }

    //    OnKeysUpdated();
    //}

    public bool IsUnlocked => IsUnlockedByKey(activeKey);

    bool IsUnlockedByKey(InventoryKey key)
    {
        if (requiredKey == InventoryKey.None)
            return true;

        //for (int i = 0; i < requiredKeys.Length; i++)
        //{
        //    if (activeKeys.Contains(requiredKeys[i], out _) == false)
        //        return false;
        //}

        return key == requiredKey;
    }

    void OnKeysUpdated()
    {
        OnSwitchUpdated?.Invoke(IsUnlocked);
        //keyChain.ShowKeys(activeKeys);
        keyChain.ShowKey(activeKey);
    }

    public List<InventoryKey> GetValidInventoryKeys()
    {
        keyOptions.Clear();

        var playerInventory = Player.Instance.inventory;

        foreach (var item in playerInventory)
        {
            if (item.Value < 1)
            {
                continue;
            }

            if (validKeyOptions == null || validKeyOptions.Length == 0)
            {
                keyOptions.Add(item.Key);
                continue;
            }

            if (validKeyOptions.Contains(item.Key, out _))
            {
                keyOptions.Add(item.Key);
            }
        }

        keyOptions.Add(InventoryKey.None);
        return keyOptions;
    }

    public bool AreRequirementsMet(out int current, out int total, out bool isMisc)
    {
        current = 0; total = 0; isMisc = false;

        if (requiredItemInInventory.Length == 0)
            return true;

        var inventory = Player.Instance.inventory;
        for (int i = 0; i < requiredItemInInventory.Length; i++)
        {
            var req = requiredItemInInventory[i];
            total = total + (req.amount == 0 ? 1 : req.amount);
            isMisc = req.key == InventoryKey.MiscBall;
            if (inventory.TryGetValue(req.key, out var amount) == false)
            {
                continue;
            }
            current = current + (amount == 0 ? 1 : amount);
        }

        return current >= total;
    }
}
