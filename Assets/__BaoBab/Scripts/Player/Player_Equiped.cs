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
    /// 보조무기
    /// </summary>
    public SubWeaponBase subweapon;

    /// <summary>
    /// 착용중인 장비 리스트
    /// </summary>
    public EquiptBase[] Equipments;

    /// <summary>
    /// 이전 장비 인덱스 int
    /// </summary>
    int previousHold = 0;

    /// <summary>
    /// 마우스 십자선 transform
    /// </summary>
    public Transform MouseCross;

    /// <summary>
    /// 무기 슬롯
    /// </summary>
    public Transform weaponSlot;


    /// <summary>
    /// 마우스 민감도 조절용 float
    /// </summary>
    public float mouseSenctive;

    /// <summary>
    /// 화면 영역지정용 Vector2변수
    /// </summary>
    public Vector2 CrossMoveAria;

    public Action EatGear;

    PlayerInput input;

    #region 프로퍼티

    /// <summary>
    /// 무기가 사용 가능한 상태인지 체크하는 bool(프로퍼티 있음)
    /// </summary>
    bool canUseWeapon = true;

    /// <summary>
    /// 무기가 사용 가능한 상태인지 정하는 프로퍼티
    /// </summary>
    public bool CanUseWeapon
    {
        get
        {
            return canUseWeapon;
        }
        set
        {
            if (canUseWeapon != value)
            {
                canUseWeapon = value;
                if (canUseWeapon)
                {
                    weaponSlot.gameObject.SetActive(true);
                }
                else
                {
                    weaponSlot.gameObject.SetActive(false);
                }
            }
        }
    }


    /// <summary>
    /// 들고있는 장비 index 0번은 맨손(프로퍼티 있음)
    /// </summary>
    public int nowHold = 0;

    /// <summary>
    /// 들고있는 장비를 정하는 인덱스 프로퍼티
    /// </summary>
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
                if (value > 2)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = 2;
                }
                nowHold = value;
                HoldThisGearToPress(nowHold);
                previousHold = nowHold;
            }
        }
    }

    /// <summary>
    /// 마우스 delta값에 연결된 Vector3 변수(프로퍼티 있음)
    /// </summary>
    Vector3 mouseMoving;

    /// <summary>
    /// 마우스 delta값에 따라 십자선을 움직이게 하는 프로퍼티
    /// </summary>
    Vector3 MouseMoving
    {
        get
        {
            return mouseMoving;
        }
        set
        {
            mouseMoving = value;
            Vector3 newpos = MouseCross.localPosition + (mouseMoving * Time.fixedDeltaTime * mouseSenctive);
            /// 십자선이 캐릭터의 왼쪽에 있는지 오른쪽에 있는지 비교하기 위한 십자선의 위치를 월드값으로 변환하는 작업(나중에 수정할수도 있음)
            Vector3 testpos = MouseCross.transform.TransformPoint(MouseCross.localPosition);
            newpos.x = MouseOnScreen(newpos.x, CrossMoveAria.x);
            newpos.y = MouseOnScreen(newpos.y, CrossMoveAria.y);
            newpos.z = 10f;
            MouseCross.localPosition = newpos;
            if (testpos.x > this.transform.transform.position.x)
            {
                weaponSlot.localRotation = Quaternion.LookRotation(Vector3.forward, newpos) * Quaternion.Euler(0, 0, 90);
                if (Equipments[NowHold] != null)
                {
                    // Equipments[NowHold].spRender.flipX = false;
                    // 콜라이더 위치가 sprite가 뒤집어 졌을 때도 같은 위치에 적용 되게 하기 위해 수정했습니다.
                    Equipments[NowHold].spRender.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                weaponSlot.localRotation = Quaternion.LookRotation(Vector3.forward, newpos) * Quaternion.Euler(0, 0, -90);
                if (Equipments[NowHold] != null)
                {
                    // Equipments[NowHold].spRender.flipX = true;
                    Equipments[NowHold].spRender.transform.localScale = new Vector3(-1, 1, 1);

                }
            }
        }
    }
    #endregion


    private void Awake()
    {
        input = new PlayerInput();
        Equipments = new EquiptBase[4];
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.MouseMove.performed += MoveingMouse;
        input.Player.MouseAction.performed += UseHold;
        input.Player.MouseAction.canceled += UnUseHold;

        input.Player.MouseUtility.performed += UseUtill;
        input.Player.MouseUtility.canceled += UnUseUtill;

        input.Player.GearSellect1.performed += GearSellect1;
        input.Player.GearSellect2.performed += GearSellect2;
        input.Player.GearSellect3.performed += GearSellect3;

        input.Player.MouseScroll.performed += MouseScrollToChangeWeapon;

        input.Player.UseSubWeapon.performed += UseSubWeaponNow;
    }

    private void OnDisable()
    {
        input.Player.UseSubWeapon.performed -= UseSubWeaponNow;

        input.Player.MouseScroll.performed -= MouseScrollToChangeWeapon;

        input.Player.GearSellect3.performed -= GearSellect3;
        input.Player.GearSellect2.performed -= GearSellect2;
        input.Player.GearSellect1.performed -= GearSellect1;

        input.Player.MouseUtility.canceled -= UnUseUtill;
        input.Player.MouseUtility.performed -= UseUtill;

        input.Player.MouseAction.canceled -= UnUseHold;
        input.Player.MouseAction.performed -= UseHold;
        input.Player.MouseMove.performed -= MoveingMouse;
        input.Disable();
    }
    private void Start()
    {
        InventoryInfo.Inst.ListHasBeenChanged += () =>
        {
            RefreshTheList(InventoryInfo.Inst.equipinven);
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            EquiptBase equipt = collision.gameObject.GetComponent<EquiptBase>();
            PickUpWeapon(equipt);
        }
        if (collision.CompareTag("SubWeapon") && subweapon == null)
        {
            SubWeaponBase equiptsub = collision.gameObject.GetComponent<SubWeaponBase>();
            PickUpSubWeapon(equiptsub);
        }
    }

    /// <summary>
    /// 보조무기 획득 함수
    /// </summary>
    void PickUpSubWeapon(SubWeaponBase supb)
    {
        supb.gameObject.transform.parent = weaponSlot;
        subweapon = supb;
        subweapon.EquiptSub?.Invoke();
        subweapon.gameObject.SetActive(false);
    }

    /// <summary>
    /// 무기를 배열에 추가하는 함수
    /// </summary>
    /// <param name="weapon">배열에 추가할 무기 컴포넌트</param>
    void PickUpWeapon(EquiptBase weapon)
    {
        int holder = 5;
        bool successfulyOnLoad = false;
        for (int i = 0; i < 3; i++)
        {
            if (Equipments[i] == null)
            {
                Equipments[i] = weapon;
                weapon.gameObject.transform.parent = weaponSlot;
                Equipments[i].EquiptThis?.Invoke();
                successfulyOnLoad = true;
                holder = i;
                break;
            }
            else
            {
                if (Equipments[i].gameObject == weapon.gameObject)
                {
                    break;
                }
            }
        }
        if (successfulyOnLoad)
        {
            NowHold = holder;
        }
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
        if (Equipments[NowHold] != null && CanUseWeapon)
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
        if (Equipments[NowHold] != null && CanUseWeapon)
        {
            Equipments[NowHold].StopUseAction?.Invoke();
        }
    }

    /// <summary>
    /// 사용 시작 마우스 우클릭 입력
    /// </summary>
    /// <param name="obj"></param>
    private void UseUtill(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Equipments[NowHold] != null && CanUseWeapon)
        {
            Equipments[NowHold].UtillAction?.Invoke();
        }
    }

    /// <summary>
    /// 사용 끝 마우스 우클릭 입력
    /// </summary>
    /// <param name="obj"></param>
    private void UnUseUtill(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Equipments[NowHold] != null && CanUseWeapon)
        {
            Equipments[NowHold].StopUtillAction?.Invoke();
        }
    }

    /// <summary>
    /// 장비 선택 함수
    /// </summary>
    /// <param name="index">장비 인덱스</param>
    void HoldThisGearToPress(int index)
    {
        //전에 들고있던 장비 숨김
        if (Equipments[previousHold] != null)
        {
            Equipments[previousHold].gameObject.SetActive(false);
        }
        //지금 들 장비를 활성화
        if (Equipments[index] != null)
        {
            Equipments[index].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 인벤토리에서 순서를 교체하면 실행될 재배열 함수
    /// </summary>
    /// <param name="list"></param>
    public void RefreshTheList(EquiptBase[] list)
    {
        for (int i = 0; i < 3; i++)
        {
            if (list[i] != null)
            {
                Equipments[i] = list[i];
            }
            else
            {
                Equipments[i] = null;
            }
        }
    }

    /// <summary>
    /// 숫자패드 무기 교체
    /// </summary>
    /// <param name="obj"></param>
    private void GearSellect1(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 0;
    }
    private void GearSellect2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 1;
    }
    private void GearSellect3(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NowHold = 2;
    }

    /// <summary>
    /// 보조무기 사용
    /// </summary>
    /// <param name="obj"></param>
    private void UseSubWeaponNow(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /*        if (Equipments[NowHold] != null)
                {
                    Equipments[NowHold].gameObject.SetActive(false);
                }*/
        subweapon.gameObject.SetActive(true);
        subweapon.UseSubWeapon?.Invoke();
    }

    /// <summary>
    /// 마우스 스크롤 무기 교체
    /// </summary>
    /// <param name="obj"></param>
    private void MouseScrollToChangeWeapon(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 test = obj.ReadValue<Vector2>();
        if (test.y > 0)
        {
            NowHold++;
        }
        else
        {
            NowHold--;
        }
    }
}
