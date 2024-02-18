using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    void Start()
    {
        FPSController.allowInput = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FPSController.allowInput = true;
            gameObject.SetActive(false);
        }
    }
}
