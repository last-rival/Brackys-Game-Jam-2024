using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PillarController : MonoBehaviour
{
    [SerializeField]
    bool isUnlocked;

    public Vector3 moveDelta;

    public float inTime = 2f;
    public Ease inEase = Ease.OutBack;
    public float inInterval = 0.35f;

    public float outTime = 1f;
    public Ease outEase = Ease.InQuint;
    public float outInterval = 0.15f;

    [SerializeField]
    Transform[] pillars;

    Vector3[] initialPositions;
    Vector3[] finalPositions;

    bool isMoving;

    void Start()
    {
        initialPositions = new Vector3[pillars.Length];
        finalPositions = new Vector3[pillars.Length];
        for (int i = 0; i < pillars.Length; i++)
        {
            initialPositions[i] = pillars[i].position;
            finalPositions[i] = initialPositions[i] + moveDelta;
        }
    }

    public void OnKeyChanged(bool isUnlocked)
    {
        if (this.isUnlocked != isUnlocked)
        {
            this.isUnlocked = isUnlocked;

            if (isMoving)
            {
                StopAllCoroutines();
            }

            if (isUnlocked)
            {
                StartCoroutine(AnimatePillarsToTarget(finalPositions, inEase, inTime, inInterval));
            }
            else
            {
                StartCoroutine(AnimatePillarsToTarget(initialPositions, outEase, outTime, outInterval));
            }
        }
    }

    IEnumerator AnimatePillarsToTarget(Vector3[] positions, Ease ease, float tweenTime, float interval)
    {



        if (isMoving)
        {
            for (int i = 0; i < pillars.Length; i++)
            {
                pillars[i].DOKill();
            }
        }

        isMoving = true;
        var wait = new WaitForSeconds(interval);

        for (int i = 0; i < pillars.Length; i++)
        {
            var targetPos = positions[i];
            var pillar = pillars[i];

            if (Vector3.Distance(pillar.position, targetPos) <= 0.05f)
            {
                pillar.position = targetPos;
                continue;
            }

            pillar.DOMove(targetPos, tweenTime).SetEase(ease);
            yield return wait;
        }

        yield return wait;
        isMoving = false;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (pillars == null || pillars.Length == 0)
            return;

        for (int i = 0; i < pillars.Length; i++)
        {
            if (pillars[i] == null)
                continue;

            var pos = pillars[i].position;
            Debug.DrawLine(pos, pos + moveDelta, Color.white);
        }
    }
#endif
}
