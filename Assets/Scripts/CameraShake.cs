using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    public void Shake()
    {
        cinemachineImpulseSource.GenerateImpulse();
    }

}
