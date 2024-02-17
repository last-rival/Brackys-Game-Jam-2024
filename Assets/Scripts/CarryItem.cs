using System;
using UnityEngine;
using UnityEngine.Events;

public interface ICarryItem
{
    void OnCarry(Transform parent);
    void OnDrop(Transform parent);
}

[Serializable]
public class ItemCarryEvent : UnityEvent<InventoryKey> { }

public class CarryItem : MonoBehaviour, ICarryItem
{
    [SerializeField]
    public InventoryKey Key;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    ItemCarryEvent onCarry;

    [SerializeField]
    ItemCarryEvent onDrop;

    [SerializeField]
    PortalSwitch interaction;

    public void OnPlayerInteraction(bool isOn)
    {
        if (rb.isKinematic == false)
        {
            Player.Instance.Carry(transform);
            Player.Instance.AddKey(Key);
            interaction.enabled = false;
        }
    }

    public void OnCarry(Transform parent)
    {
        rb.isKinematic = true;
        onCarry?.Invoke(Key);
    }

    public void OnDrop(Transform parent)
    {
        rb.isKinematic = false;
        onDrop?.Invoke(Key);
    }
}
