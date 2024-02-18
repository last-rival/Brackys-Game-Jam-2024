using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PortalConnectionHUB : MonoBehaviour
{
    [Serializable]
    public struct PortakKeyMap
    {
        public InventoryKey[] keys;
        public Portal portal;
    }

    public Portal connectedPortal;
    public InventoryItemSwitchInteraction[] interactions;

    public PortakKeyMap[] map;

    void Start()
    {
        foreach (var interaction in interactions)
        {
            interaction.OnKeySet.AddListener(OnConnectionUpdated);
        }
    }

    public void OnConnectionUpdated(InventoryKey _)
    {
        ClearPortal();

        for (int i = 0; i < map.Length; i++)
        {
            PortakKeyMap path = map[i];
            bool isSet = true;
            foreach (var key in path.keys)
            {
                bool any = false;
                foreach (var interaction in interactions)
                {
                    if (interaction.selectedKey == key)
                    {
                        any = true;
                        break;
                    }
                }

                if (any == false)
                {
                    isSet = false;
                }
            }

            if (isSet)
            {
                ConnectToPortal(path.portal);
                break;
            }
        }
    }

    void ConnectToPortal(Portal portal)
    {
        connectedPortal.linkedPortal = portal;
        portal.linkedPortal = connectedPortal;

        connectedPortal.UpdateViewTexture();
        portal.UpdateViewTexture();
    }

    private void ClearPortal()
    {
        if (connectedPortal.linkedPortal != null)
        {
            connectedPortal.linkedPortal.linkedPortal = null;
        }
        connectedPortal.linkedPortal = null;
    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        if (map == null)
            return;

        if (connectedPortal == null)
            return;

        var pos = connectedPortal.transform.position;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i].portal == null)
            {
                continue;
            }

            var linkedPos = map[i].portal.transform.position;
            var dir = linkedPos - pos;
            Handles.color = Color.white;
            Handles.DrawLine(pos + Vector3.up, linkedPos + Vector3.up);
            Handles.color = Color.grey;
            Handles.ArrowHandleCap(0, pos + Vector3.up, Quaternion.LookRotation(dir, transform.up), 1, EventType.Repaint);
        }

        pos = transform.position;

        for (int i = 0; i < interactions.Length; i++)
        {
            if (interactions[i] == null)
            {
                continue;
            }

            var linkedPos = interactions[i].transform.position;
            var dir = linkedPos - pos;
            Handles.color = Color.white;
            Handles.DrawLine(pos, linkedPos);
            Handles.color = Color.magenta;
            Handles.ArrowHandleCap(0, pos, Quaternion.LookRotation(dir, transform.up), 1, EventType.Repaint);
        }

        var linkedPos2 = connectedPortal.transform.position;
        var dir2 = linkedPos2 - pos;
        Handles.color = Color.white;
        Handles.DrawLine(pos, linkedPos2 + Vector3.up);
        Handles.color = Color.yellow;
        Handles.ArrowHandleCap(0, pos, Quaternion.LookRotation(dir2 + Vector3.up, transform.up), 1, EventType.Repaint);
    }

#endif
}
