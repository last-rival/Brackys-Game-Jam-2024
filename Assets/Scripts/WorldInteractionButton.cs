using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class WorldInteractionButton : MonoBehaviour
{
    [SerializeField]
    public bool isInteractable = true;

    [SerializeField]
    InteractionKey interactionKey = InteractionKey.None;

    [SerializeField]
    bool autoInteract;

    [SerializeField]
    Transform interactionAnchor;

    [SerializeField]
    bool isOn;

    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;
    public WorldButtonClickEvent OnSwitchClicked;

    void Start()
    {
        if (interactionAnchor == null)
        {
            interactionAnchor = transform;
        }

        enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isInteractable == false)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Player") == false)
            return;

        if (autoInteract)
        {
            ToggleState();
            return;
        }

        OnTriggerEnterEvent?.Invoke();

        UI_Manager.Instance.ShowInteractionButton(interactionKey, interactionAnchor, true);
        enabled = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (isInteractable == false)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Player") == false)
            return;

        if (autoInteract)
        {
            return;
        }

        OnTriggerEnterEvent?.Invoke();

        UI_Manager.Instance.ShowInteractionButton(interactionKey, interactionAnchor, false);
        enabled = false;
    }
    void Update()
    {
        if (isInteractable == false)
        {
            return;
        }

        if (interactionKey == InteractionKey.None || interactionKey >= InteractionKey.Extra)
        {
            return;
        }

        if ((int)interactionKey > (int)InteractionKey.RightClick)
        {
            if (Input.GetKeyDown((KeyCode)interactionKey))
            {
                ToggleState();
                return;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown((int)interactionKey))
            {
                ToggleState();
            }
        }
    }

    void ToggleState()
    {
        isOn = !isOn;
        OnSwitchClicked?.Invoke(isOn);
    }
}

[Serializable]
public class WorldButtonClickEvent : UnityEvent<bool> { }

[Serializable]
public enum InteractionKey
{
    None = -1,
    LeftClick = 0,
    RightClick = 1,
    F = KeyCode.F,
    E = KeyCode.E,
    Extra,
    MouseWheel,
}