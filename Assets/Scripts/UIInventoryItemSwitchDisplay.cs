using UnityEngine;

public class UIInventoryItemSwitchDisplay : MonoBehaviourInstance<UIInventoryItemSwitchDisplay>
{
    [SerializeField]
    GameObjectBillboard billboard;

    void OnEnabled()
    {
        billboard.enabled = true;
    }

    void OnDisabled()
    {
        billboard.enabled = false;
    }

    public void ShowDisplayAt(Transform anchor)
    {
        transform.SetParent(anchor, false);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void HideDisplay()
    {
        gameObject.SetActive(false);
    }
}

