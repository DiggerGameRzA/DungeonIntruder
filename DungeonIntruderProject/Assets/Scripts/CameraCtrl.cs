using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private GameObject mainCM;
    [SerializeField] private GameObject subCM;

    private void Update()
    {
        if (FindObjectOfType<Player>())
        {
            // mainCM.SetActive(true);
        }
        else
        {
            // mainCM.SetActive(false);
        }
    }
}
