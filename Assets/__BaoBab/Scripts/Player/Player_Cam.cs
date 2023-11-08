using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineTransposer transposer;

    public Vector3 mousePosition;
    public float moveMulty;
    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePosition.z = -2;
        transposer.m_FollowOffset = mousePosition * moveMulty;
    }
}
