using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractionButtonPrompt : MonoBehaviour
{
    public TextMeshProUGUI label;

    public void SetImage(string text)
    {
        label.SetText(text);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
