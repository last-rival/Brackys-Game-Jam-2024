﻿using UnityEngine;

public class InventoryItemSwitch : MonoBehaviour
{
    //[SerializeField]
    //public InventoryKey[] requiredKeys;
    //[SerializeField]
    //public InventoryKey[] activeKeys;

    [SerializeField]
    public InventoryKey requiredKey;
    [SerializeField]
    public InventoryKey activeKey;

    [SerializeField]
    public KeyChain keyChain;

    public WorldButtonClickEvent OnSwithUpdated;

    void Start()
    {
        //activeKeys = new InventoryKey[requiredKeys.Length];
    }

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

    public bool IsUnlocked()
    {
        if (requiredKey == InventoryKey.None)
            return true;

        //for (int i = 0; i < requiredKeys.Length; i++)
        //{
        //    if (activeKeys.Contains(requiredKeys[i], out _) == false)
        //        return false;
        //}

        return activeKey == requiredKey;
    }

    void OnKeysUpdated()
    {
        OnSwithUpdated?.Invoke(IsUnlocked());
        //keyChain.ShowKeys(activeKeys);
        keyChain.ShowKey(activeKey);
    }
}
