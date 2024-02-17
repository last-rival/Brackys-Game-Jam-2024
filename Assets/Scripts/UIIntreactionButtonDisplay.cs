using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntreactionButtonDisplay : MonoBehaviour
{
    public Camera cam;
    public Canvas canvas;
    [SerializeField]
    List<UIInteractionButtonPrompt> buttonImageBuffer;

    List<(KeyCode key, Transform anchor)> interactionPoints = new List<(KeyCode key, Transform anchor)>(8);

    public void ShowInteractionButton(KeyCode key, Transform worldAnchor, bool show)
    {
        if (show)
        {
            if (interactionPoints.Contains((key, worldAnchor)))
            {
                return;
            }

            interactionPoints.Add((key, worldAnchor));
            canUpdate = true;
            return;
        }

        int index = interactionPoints.IndexOf((key, worldAnchor));
        interactionPoints.RemoveAtSwapBack(index);
    }


    private bool canUpdate;

    private void Update()
    {

        if (!canUpdate)
        {
            return;
        }

        int imageIndex = 0;
        for (int i = 0; i < interactionPoints.Count; i++)
        {
            var interactionPoint = interactionPoints[i];
            var dot = Vector3.Dot(cam.transform.forward, (interactionPoint.anchor.position - cam.transform.position).normalized);

            if (dot < 0)
                continue;

            var screenPos = cam.WorldToScreenPoint(interactionPoint.anchor.position);
            var uiPoint = canvas.transform.InverseTransformPoint(screenPos);

            buttonImageBuffer[imageIndex].SetImage(interactionPoint.key.ToString());
            buttonImageBuffer[imageIndex].transform.localPosition = uiPoint;
            imageIndex++;
        }

        for (int i = imageIndex; i < buttonImageBuffer.Count; i++)
        {
            if (buttonImageBuffer[i].gameObject.activeSelf)
            {
                buttonImageBuffer[i].Hide();
            }
        }

        canUpdate = interactionPoints.Count > 0;
    }

}
