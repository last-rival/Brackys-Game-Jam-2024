using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Inventory Data", menuName = "Custom/Inventory Data")]
public class InventoryKeyData : ScriptableObject
{
    public List<InventoryItemData> data;

    public bool TryGetDataForKey(InventoryKey key, out InventoryItemData itemData)
    {
        for (int i = 0; i < data.Count; i++)
        {
            itemData = data[i];
            if (itemData.key==key)
                return true;
        }

        itemData = null;
        return false;
    }
}


[System.Serializable]
public class InventoryItemData
{
    public InventoryKey key;
    public Sprite image;
    public Color color = Color.white;
}
