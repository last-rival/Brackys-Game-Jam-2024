using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Icon Data", menuName = "Custom/Interaction Icon Data")]
public class UIInteractionButtonIconData : ScriptableObject
{
    [SerializeField]
    InteractionKeySpritePair defaultData;

    public List<InteractionKeySpritePair> data;

    public bool TryGetDataForKey(InteractionKey key, out InteractionKeySpritePair itemData)
    {
        for (int i = 0; i < data.Count; i++)
        {
            itemData = data[i];
            if (itemData.key == key)
                return true;
        }

        itemData = defaultData;
        itemData.key = key;
        return false;
    }
}


[System.Serializable]
public class InteractionKeySpritePair
{
    public InteractionKey key;
    public Sprite sprite;
}