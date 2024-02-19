using System.Collections.Generic;
using UnityEngine;

public class CollectionGlobalVisualManager : MonoBehaviour
{
    public GameObject[] collectibles;

    bool[] localStates;

    static bool isDirty;
    static bool[] sharedState;

    static List<CollectionGlobalVisualManager> syncs = new List<CollectionGlobalVisualManager>();

    void Awake()
    {
        syncs.Add(this);
    }

    void Start()
    {
        localStates = new bool[collectibles.Length];
        for (int i = 0; i < collectibles.Length; i++)
            localStates[i] = collectibles[i].activeSelf;

        if (sharedState == null || sharedState.Length == 0)
        {
            sharedState = new bool[collectibles.Length];
            for (int i = 0; i < collectibles.Length; i++)
            {
                sharedState[i] = localStates[i];
            }
            isDirty = true;
        }
    }

    void Update()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (localStates[i] != collectibles[i].activeSelf)
            {
                localStates[i] = collectibles[i].activeSelf;
                sharedState[i] = localStates[i];
                isDirty = true;
            }
        }
    }

    void LateUpdate()
    {
        if (isDirty)
        {
            UpdateAll();
            isDirty = false;
        }
    }

    public static void UpdateAll()
    {
        foreach (var sync in syncs)
        {
            sync.SetStateFromGlobal();
        }
    }

    public void SetStateFromGlobal()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i]?.SetActive(sharedState[i]);
        }
    }
}
