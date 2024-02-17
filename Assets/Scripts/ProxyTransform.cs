using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyTransform : MonoBehaviour
{
    //TODO : Make big manager to control group of objects positions
    [SerializeField]
    private Transform target;

    void Update()
    {
        target.position=transform.position;
    }
}
