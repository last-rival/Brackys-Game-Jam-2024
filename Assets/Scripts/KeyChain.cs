using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChain : MonoBehaviour
{
    [System.Serializable]
    struct KeyVisualPair
    {
        public InventoryKey key;
        public GameObject visual;
    }

    [SerializeField]
    KeyVisualPair[] keys;

    public void ShowKeys(InventoryKey[] activeKeys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            var pair = keys[i];
            pair.visual.gameObject.SetActive(activeKeys.Contains(pair.key, out _));
        }
    }

    public void ShowKey(InventoryKey activeKey)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            var pair = keys[i];
            pair.visual.gameObject.SetActive(activeKey == pair.key);
        }
    }
}
