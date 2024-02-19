using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HeavenGatePath : MonoBehaviour
{
    [System.Serializable]
    public struct FloatRange
    {
        public float min, max;
        public Ease ease;

        public float GetValue(float value)
        {
            return DOVirtual.EasedValue(min, max, value, ease);
        }
    }


    public FloatRange bloom;
    public FloatRange chormatic;

    public PostProcessVolume volume;
    public Transform player;
    public HeavenGateStart start;
    public HeavenGateEnd end;

    public CanvasGroup toBlack;

    public static bool inGate;

    void Update()
    {
        if (inGate == false)
        {
            return;
        }

        float total = Vector3.Distance(start.transform.position, end.transform.position);
        float current = Vector3.Distance(start.transform.position, player.position);
        volume.profile.GetSetting<Bloom>().intensity.value = bloom.GetValue(current / total);
        volume.profile.GetSetting<ChromaticAberration>().intensity.value = chormatic.GetValue(current / total);
        toBlack.alpha = (current / total);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false)
            return;

        inGate = false;

        volume.profile.GetSetting<Bloom>().intensity.value = 0;
        volume.profile.GetSetting<ChromaticAberration>().intensity.value = 0;
        toBlack.alpha = 0;
    }
}
