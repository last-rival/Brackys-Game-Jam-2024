using UnityEngine;

public class MainCamera : MonoBehaviourInstance<MainCamera>
{
    public Camera Camera;
    public Portal[] portals;

    protected override void OnAwake()
    {
        portals = FindObjectsOfType<Portal>();
    }

    void OnPreCull()
    {

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PrePortalRender();
        }

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PostPortalRender();
        }

    }

}