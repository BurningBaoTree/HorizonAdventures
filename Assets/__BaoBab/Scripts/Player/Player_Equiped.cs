using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 공격과 장비 관리는 이 코드에서 실행
/// </summary>
public class Player_Equiped : MonoBehaviour
{
    /// <summary>
    /// 착용중인 장비 리스트
    /// </summary>
    public List<EquiptBase> Equipments = new List<EquiptBase>(5);

    /// <summary>
    /// 들고있는 장비 index 0번은 맨손
    /// </summary>
    int nowHold = 0;
    public int NowHold
    {
        get
        {
            return nowHold;
        }
        set
        {
            if (nowHold != value)
            {
                nowHold = value;
            }
        }
    }


    /// <summary>
    /// 마우스 십자선 transform
    /// </summary>
    public Transform MouseCross;


    public Transform weaponSlot;


    /// <summary>
    /// 마우스 민감도 조절용 float
    /// </summary>
    public float mouseSenctive;


    /// <summary>
    /// 마우스 delta값에 따라 십자선을 움직이게 하는 프로퍼티
    /// </summary>
    Vector3 mouseMoving;
    Vector3 MouseMoving
    {
        get
        {
            return mouseMoving;
        }
        set
        {
            if (mouseMoving != value)
            {
                mouseMoving = value;
                Vector3 newpos = MouseCross.localPosition + (mouseMoving * Time.fixedDeltaTime * mouseSenctive);
                newpos.x = MouseOnScreen(newpos.x, 7.17f);
                newpos.y = MouseOnScreen(newpos.y, 4.4f);
                newpos.z = 10f;
                MouseCross.localPosition = newpos;
                weaponSlot.localRotation = Quaternion.LookRotation(Vector3.forward, newpos) * Quaternion.Euler(0, 0, 90);
            }
        }
    }

    public bool OnTheRun = true;

    Action updater;
    PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        updater = () => { };
        input.Enable();
        input.Player.MouseMove.performed += MoveingMouse;
        input.Player.MouseAction.performed += UseHold;
        input.Player.MouseAction.canceled += UnUseHold;

        input.Player.GearSellect1.performed += GearSellect1;
        input.Player.GearSellect2.performed += GearSellect2;
        input.Player.GearSellect3.performed += GearSellect3;
        input.Player.GearSellect4.performed += GearSellect4;
    }



    private void OnDisable()
    {
        input.Player.GearSellect4.performed -= GearSellect4;
        input.Player.GearSellect3.performed -= GearSellect3;
        input.Player.GearSellect2.performed -= GearSellect2;
        input.Player.GearSellect1.performed -= GearSellect1;

        input.Player.MouseAction.canceled -= UnUseHold;
        input.Player.MouseAction.performed -= UseHold;
        input.Player.MouseMove.performed -= MoveingMouse;
        input.Disable();
    }
    private void Start()
    {
        Cursor.lockState = OnTheRun ? CursorLockMode.Locked : CursorLockMode.None;
    }
    private void Update()
    {
        updater();
    }

    /// <summary>
    /// 십자선 화면 바깥으로 못나가게 잠그는 함수
    /// </summary>
    /// <param name="income">입력값</param>
    /// <param name="Holdfloat">제한값</param>
    /// <returns></returns>
    static float MouseOnScreen(float income, float Holdfloat)
    {
        return Mathf.Clamp(income, -Holdfloat, Holdfloat);
    }

    /// <summary>
    /// 십자선 움직이는 마우스 delta입력
    /// </summary>
    /// <param name="obj"></param>
    private void MoveingMouse(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MouseMoving = obj.ReadValue<Vector2>();
    }

    /// <summary>
    /// 사용 시작 마우스 좌클릭 입력
    /// </summary>
    /// <param name="obj"></param>
    private void UseHold(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Equipments[NowHold] != null)
        {
            Equipments[NowHold].UseAction?.Invoke();
        }

    }

    /// <summary>
    /// 사용 끝 마우스 좌클릭 입력
    /// </summary>
    /// <param name="obj"></param>
    private void UnUseHold(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Equipments[NowHold] != null)
        {
            Equipments[NowHold].UtillAction?.Invoke();
        }
    }


    private void GearSellect1(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 1;
    }
    private void GearSellect2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 2;
    }
    private void GearSellect3(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 3;
    }
    private void GearSellect4(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 4;
    }
}
