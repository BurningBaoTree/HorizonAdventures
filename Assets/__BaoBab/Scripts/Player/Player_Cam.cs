using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineTransposer transposer;

    public bool AimCamMove = false;


    public Vector3 mousePosition;
    public Vector3 MousePosition
    {
        get
        {
            return mousePosition;
        }
        set
        {
            if(mousePosition != value)
            {
                mousePosition = value;
                mousePosition.z = -2;
                if (AimCamMove)
                {
                    transposer.m_FollowOffset = mousePosition * moveMulty;
                }
                else
                {

                }
            }
        }
    }

    public float moveMulty;
    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void Update()
    {
        MousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
}
