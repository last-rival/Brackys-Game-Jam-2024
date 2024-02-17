using System;
using UnityEngine;
using UnityEngine.Events;

public interface ICarryItem
{
    void OnCarry(Transform parent);
    void OnDrop(Transform parent);
}

[Serializable]
public class InventoryItemCarryEvent : UnityEvent<InventoryKey> { }

public class InventoryKeyItemPickup : MonoBehaviour, ICarryItem
{
    [SerializeField]
    public InventoryKey Key;

    //[SerializeField]
    Rigidbody rb;

    //[SerializeField]
    InventoryItemCarryEvent onCarry;

    //[SerializeField]
    InventoryItemCarryEvent onDrop;

    [SerializeField]
    WorldInteractionButton interaction;

    public void OnPlayerInteraction(bool _)
    {
        interaction.enabled = false;
        Player.Instance.AddKey(Key);
        gameObject.SetActive(false);
    }

    public void OnPlayerInteractionCarry(bool _)
    {
        interaction.enabled = false;
        Player.Instance.AddKey(Key);
        Player.Instance.Carry(transform);
    }

    public void OnCarry(Transform parent)
    {
        if (rb)
            rb.isKinematic = true;

        Player.Instance.AddKey(Key);
        onCarry?.Invoke(Key);
    }

    public void OnDrop(Transform parent)
    {
        if (rb)
            rb.isKinematic = false;

        Player.Instance.UseKey(Key);
        onDrop?.Invoke(Key);
    }
}
