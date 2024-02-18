using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySwitchRequirementsDisplay : MonoBehaviour
{
    [SerializeField]
    public GameObject[] onItems;

    [SerializeField]
    public GameObject[] offItems;

    public void ShowRequirements(int current, int total, bool misc)
    {
        for (int i = 0; i < total; i++)
        {
            onItems[i].SetActive(i < current);
            offItems[i].SetActive(i >= current);
        }
    }
}
