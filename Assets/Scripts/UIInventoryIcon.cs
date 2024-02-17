using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryIcon : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI amountLabel;

    public InventoryKey key;

    public void SetInventoryIcon(InventoryItemData data, int count)
    {
        key = data.key;
        icon.sprite = data.image;
        icon.color = data.color;

        amountLabel.SetText(count.ToString());
    }
}
