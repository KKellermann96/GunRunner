using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera vcam1;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private void Awake()
    {
        Instance = this;
        vcam1 = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmbp = vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmbp.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cmbp = vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmbp.m_AmplitudeGain = 0f;
                Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
            }
        }
    }

}
