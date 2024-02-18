using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PortalConnector : MonoBehaviour
{
    [Serializable]
    public struct PortalKeyPair
    {
        public InventoryKey key;
        public Portal portal;
    }

    [SerializeField]
    Portal connectedPortal;

    public PortalKeyPair[] portals;

    public void OnSwitchUpdated(InventoryKey activeKey)
    {
        ClearPortal();

        if (activeKey == InventoryKey.None)
        {
            return;
        }

        for (int i = 0; i < portals.Length; i++)
        {
            var pair = portals[i];
            if (pair.key != activeKey)
            {
                continue;
            }

            connectedPortal.linkedPortal = pair.portal;
            pair.portal.linkedPortal = connectedPortal;
            connectedPortal.UpdateViewTexture();
            pair.portal.UpdateViewTexture();
        }

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
        if (portals == null)
            return;

        if (connectedPortal == null)
            return;

        var pos = connectedPortal.transform.position;
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i].portal == null)
            {
                continue;
            }

            var linkedPos = portals[i].portal.transform.position;
            var dir = linkedPos - pos;
            Handles.color = Color.white;
            Handles.DrawLine(pos + Vector3.up, linkedPos + Vector3.up);
            Handles.color = Color.grey;
            Handles.ArrowHandleCap(0, pos + Vector3.up, Quaternion.LookRotation(dir, transform.up), 1, EventType.Repaint);
        }


        pos = transform.position;
        var linkedPos2 = connectedPortal.transform.position;
        var dir2 = linkedPos2 - pos;
        Handles.color = Color.white;
        Handles.DrawLine(pos, linkedPos2 + Vector3.up);
        Handles.color = Color.yellow;
        Handles.ArrowHandleCap(0, pos, Quaternion.LookRotation(dir2 + Vector3.up, transform.up), 1, EventType.Repaint);
    }

#endif

}
