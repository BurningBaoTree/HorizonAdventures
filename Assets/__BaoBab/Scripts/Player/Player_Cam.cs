using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 마우스를 따라가는 카메라 코드
/// </summary>
public class Player_Cam : MonoBehaviour
{
    /// <summary>
    /// 버츄얼 카메라
    /// </summary>
    CinemachineVirtualCamera vcam;

    /// <summary>
    /// 버츄얼 카메라 transposer항목
    /// </summary>
    CinemachineTransposer transposer;

    /// <summary>
    /// 카메라가 에임을 따라가게 만드는 bool
    /// </summary>
    public bool AimCamMove = false;

    /// <summary>
    /// 에임용 마우스 커서 local위치
    /// </summary>
    public Transform mousecorsurpos;

    /// <summary>
    /// 카메라가 마우스 커서를 따라가게 만드는 프로퍼티
    /// </summary>
    Vector3 mousePosition;
    public Vector3 MousePosition
    {
        get
        {
            return mousePosition;
        }
        set
        {
            if (mousePosition != value)
            {
                mousePosition = value;
                mousePosition.z = -10;
                if (AimCamMove)
                {
                    transposer.m_FollowOffset = mousePosition * moveMulty;
                }
            }
        }
    }

    /// <summary>
    /// 카메라가 마우스를 따라가는 정도
    /// </summary>
    public float moveMulty;


    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void Update()
    {
        MousePosition = mousecorsurpos.localPosition;
    }
}
