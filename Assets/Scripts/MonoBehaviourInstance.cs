using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Duplicate copy of {nameof(T)}", gameObject);
            return;
        }
        Instance = this as T;
        OnAwake();
    }

    protected virtual void OnAwake() { }
}
