using System;
using Unity.Cinemachine;
using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        if (cinemachineCamera)
        {
            cinemachineCamera.gameObject.SetActive(false);
        }
    }

    public void Choose()
    {
        if (cinemachineCamera)
        {
            cinemachineCamera.gameObject.SetActive(true);
        }
    }
}
