﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offsetCamera;

    [Range(0.01f, 1.0f)]
    [SerializeField] float smooth;

    void Start()
    {
        offsetCamera = transform.position - target.position;
    }

    void Update()
    {
        Vector3 CameraPosition = target.position + offsetCamera;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, CameraPosition, smooth);
        transform.position = smoothPosition;
        transform.LookAt (target);
    }
}
