using UnityEngine;

public class CollectionGlobalVisualManager : MonoBehaviour
{
    public GameObject[] collectibles;

    bool[] localStates;

    static bool isDirty;
    static bool[] sharedState;

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
            SetStateFromGlobal();
            isDirty = false;
        }
    }

    public void SetStateFromGlobal()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].SetActive(sharedState[i]);
        }
    }
}
