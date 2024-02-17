using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractionButtonIcon : MonoBehaviour
{
    public Image icon;
    public void SetImage(Sprite sprite)
    {
        icon.sprite = sprite;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
