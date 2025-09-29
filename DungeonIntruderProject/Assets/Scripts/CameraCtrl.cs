using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Player;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private GameObject mainCM;
    [SerializeField] private Transform defaultTransform;
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private void Update()
    {

    }

    public void AddTargetPlayer(Transform _transform)
    {
        targetGroup.AddMember(_transform, 1, 0);
        defaultTransform.gameObject.SetActive(false);
    }
}
