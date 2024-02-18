using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySwitchRequirementsDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite commonSprite;
    [SerializeField]
    private Sprite miscSprite;

    [SerializeField]
    TextMeshProUGUI counterText;

    [SerializeField]
    Image itemIcon;

    public void ShowRequirements(int current, int total, bool misc)
    {
        itemIcon.sprite = misc ? miscSprite : commonSprite;
        counterText.SetText("{0}/{1}", current, total);
    }
}
