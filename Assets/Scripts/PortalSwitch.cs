using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PortalSwitch : MonoBehaviour
{
    [SerializeField]
    KeyCode interactionKey = KeyCode.F;

    [SerializeField]
    Transform interactionAnchor;

    [SerializeField]
    GameObject highlight;

    [SerializeField]
    GameObject on;

    [SerializeField]
    GameObject off;

    [SerializeField]
    bool isOn;

    public PortalSwitchEvent OnSwitchClicked;

    void Start()
    {
        if (interactionAnchor == null)
        {
            interactionAnchor = transform;
        }

        SetOnState();
        enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") == false)
            return;

        if (highlight != null)
            highlight.SetActive(true);

        UI_Manager.Instance.ShowInteractionButton(interactionKey, interactionAnchor, true);
        enabled = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") == false)
            return;

        if (highlight != null)
            highlight.SetActive(false);

        UI_Manager.Instance.ShowInteractionButton(interactionKey, interactionAnchor, false);
        enabled = false;
    }

    void SetOnState()
    {
        if (on)
            on.SetActive(isOn);

        if (off)
            off.SetActive(!isOn);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            isOn = !isOn;
            SetOnState();
            OnSwitchClicked?.Invoke(isOn);
        }
    }
}

[Serializable]
public class PortalSwitchEvent : UnityEvent<bool> { }