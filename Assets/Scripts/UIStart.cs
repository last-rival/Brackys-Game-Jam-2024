using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }
}
