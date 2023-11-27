using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// 템프 슬롯 코드
/// </summary>
public class TempSlot : InventoryCon
{
    Action updater;

    /// <summary>
    /// 셀의 크기를 정하는 배열 변수
    /// </summary>
    public Vector2[] CellsizeList;

    /// <summary>
    /// 셀 내부 이미지 크기를 정하는 배열함수
    /// </summary>
    public Vector2[] ImageSizeList;

    /// <summary>
    /// 이미지의 Rect
    /// </summary>
    RectTransform sprRect;

    /// <summary>
    /// 셀의 Rect
    /// </summary>
    public RectTransform cellRect;

    /// <summary>
    /// 스프라이트 렌더러 이미지
    /// </summary>
    Image spr;

    /// <summary>
    /// 물량을 뵤여주는 TextMash
    /// </summary>
    TextMeshProUGUI textcom;

    /// <summary>
    /// 현재 갯수
    /// </summary>
    public uint countInt;

    public uint weaponSlotNum = 5;

    /// <summary>
    /// 인벤토리 Rect사이즈
    /// </summary>
    RectTransform invenRect;

    public Action sucessMoveAction;

    public bool isSucessfulyMoved = false;
    public bool IsSucessfulyMoved
    {
        get
        {
            return isSucessfulyMoved;
        }
        set
        {
            if (isSucessfulyMoved != value)
            {
                isSucessfulyMoved = value;
                if (isSucessfulyMoved)
                {
                    sucessMoveAction?.Invoke();
                }
                else
                {
                    sucessMoveAction = null;
                }
            }
        }
    }

    /// <summary>
    /// 현재 들고있는 아이템 데이터
    /// </summary>
    public ItemData copyTemInfo;

    /// <summary>
    /// 무기 데이터
    /// </summary>
    public EquiptBase copyWeaponData;

    /// <summary>
    /// 서브 웨폰 데이터
    /// </summary>
    public SubWeaponBase copySubWeaponData;

    #region 프로퍼티
    /// <summary>
    /// 마우스 추적 bool변수 (프로퍼티 있음)
    /// </summary>
    bool fallowActive = false;

    /// <summary>
    /// 템프 슬롯이 마우스를 따라가게 하는 bool 프로퍼티
    /// </summary>
    bool FallowActive
    {
        get
        {
            return fallowActive;
        }
        set
        {
            if (fallowActive != value)
            {
                fallowActive = value;
                if (fallowActive)
                {
                    updater += TempUpdate;
                }
                else
                {
                    updater -= TempUpdate;
                }
            }
        }
    }

    public bool dropMode = false;
    bool DropMode
    {
        get
        {
            return dropMode;
        }
        set
        {
            if (dropMode != value)
            {
                dropMode = value;
                if (dropMode)
                {
                    DropActive();
                }
                else
                {
                    DropDeActive();
                }
            }
        }
    }

    #endregion

    private void Awake()
    {
        updater = () => { };
        spr = transform.GetChild(0).GetComponent<Image>();
        sprRect = spr.GetComponent<RectTransform>();
        cellRect = transform.GetChild(1).GetComponent<RectTransform>();
        spr.color = Color.clear;
        textcom = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        textcom.color = Color.clear;
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ResteInfo();
        invenRect = InventoryInfo.Inst.InventoryRect;
        FallowActive = true;
    }
    private void Start()
    {
        InventoryInfo.Inst.EndDraging += EndDragTemp;
    }
    private void OnDisable()
    {
        FallowActive = false;
    }

    private void Update()
    {
        updater();
    }

    /// <summary>
    /// temp슬롯 업데이트 함수
    /// </summary>
    void TempUpdate()
    {
        transform.position = Mouse.current.position.ReadValue();
        // UI 요소의 중심 좌표가 화면 밖으로 나가면 DropMode 호출
        if (!RectTransformUtility.RectangleContainsScreenPoint(invenRect, transform.position))
        {
            DropMode = true;
        }
        else
        {
            DropMode = false;
        }
    }

    /// <summary>
    /// 아이템 정보를 temp슬롯으로 옮기는 함수
    /// </summary>
    /// <param name="tem">아이템 정보</param>
    public void LoadInfo(ItemData tem)
    {
        if (tem != null)
        {
            IsSucessfulyMoved = false;
            copyTemInfo = tem;
            spr.sprite = tem.itemIcon;
            spr.color = Color.white;
            textcom.color = Color.clear;
            countInt = 1;
            ReSizing(tem.size);
        }
    }
    public void LoadInfo(ItemData tem, uint valuint)
    {
        if (tem != null)
        {
            IsSucessfulyMoved = false;
            copyTemInfo = tem;
            spr.sprite = tem.itemIcon;
            spr.color = Color.white;
            countInt = valuint;
            textcom.text = $"{valuint: 00}";
            textcom.color = Color.white;
            ReSizing(tem.size);
        }
    }
    public void LoadInfo(EquiptBase tem)
    {
        if (tem != null)
        {
            IsSucessfulyMoved = false;
            copyWeaponData = tem;
            copyTemInfo = tem.temData;
            spr.sprite = tem.temData.itemIcon;
            spr.color = Color.white;
            textcom.color = Color.clear;
            ReSizing(tem.temData.size);
        }
    }
    public void LoadInfo(SubWeaponBase tem)
    {
        if (tem != null)
        {
            IsSucessfulyMoved = false;
            copySubWeaponData = tem;
            copyTemInfo = tem.temData;
            spr.sprite = tem.temData.itemIcon;
            spr.color = Color.white;
            textcom.color = Color.clear;
            ReSizing(tem.temData.size);
        }
    }


    /// <summary>
    /// 템프슬롯 초기화
    /// </summary>
    public void ResteInfo()
    {
        spr.sprite = null;
        spr.color = Color.clear;
        textcom.text = null;
        textcom.color = Color.clear;
        spr.gameObject.transform.localScale = Vector3.one;
        countInt = 0;
        copyTemInfo = null;
        copyWeaponData = null;
        copySubWeaponData = null;
    }

    /// <summary>
    /// 템프슬롯의 사이즈를 조절하는 함수
    /// </summary>
    /// <param name="size"></param>
    void ReSizing(ItemSize size)
    {
        sprRect.sizeDelta = ImageSizeList[(int)size];
        cellRect.sizeDelta = CellsizeList[(int)size];
    }

    /// <summary>
    /// 드래그가 종료될때 실행될 함수
    /// </summary>
    void EndDragTemp()
    {
        //드롭모드가 참이라면 드롭한다.
        Dropthis(DropMode);

        if (copySubWeaponData != null)
        {
            PatchThis(InventoryInfo.Inst.slotCellManager.ACCCanStack(copySubWeaponData));
        }
        else if (copyWeaponData != null)
        {
            PatchThis(InventoryInfo.Inst.slotCellManager.ACCCanStack(copyWeaponData));
        }
        else
        {
            PatchThis(InventoryInfo.Inst.slotCellManager.ACCCanStack(copyTemInfo));
        }
        if (weaponSlotNum != 4)
        {
            WeaponSlot slot = InventoryInfo.Inst.weaponSlots[weaponSlotNum];

            //템프 위치가 서브 위치이고 서브 무기 데이터가 존재할때, 그리고 슬롯이 비어있는 상태일때
            if (weaponSlotNum == 0 && copySubWeaponData != null && slot.isEmpty)
            {
                IsSucessfulyMoved = true;
                slot.subinitializeWeaponSlot(copySubWeaponData);
                InventoryInfo.Inst.subslot = copySubWeaponData;
                InventoryInfo.Inst.ListHasBeenChanged?.Invoke();
                gameObject.SetActive(false);
            }
            //템프 위치가 메인 위치이고 메인 무기 데이터가 존재할때, 그리고 슬롯이 비어있는 상태일때
            else if (weaponSlotNum < 4 && copyWeaponData != null && slot.isEmpty)
            {
                IsSucessfulyMoved = true;
                slot.initializeWeaponSlot(copyWeaponData);
                InventoryInfo.Inst.equipinven[weaponSlotNum-1] = copyWeaponData;
                InventoryInfo.Inst.ListHasBeenChanged?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 배치가 가능하면 배치하는 함수
    /// </summary>
    /// <param name="compar"></param>
    public void PatchThis(bool compar)
    {
        if (compar)
        {
            IsSucessfulyMoved = true;
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 물체가 버릴수 있으면 버리는 함수
    /// </summary>
    /// <param name="compar"></param>
    public void Dropthis(bool compar)
    {
        if (compar)
        {
            IsSucessfulyMoved = true;
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 드롭 가능인지 체크하는 함수
    /// </summary>
    void DropActive()
    {
        textcom.text = "Drop?";
        textcom.color = Color.white;
    }

    /// <summary>
    /// 드롭이 불가능할 경우 다시 원래 상태로 돌아오게 하는 함수
    /// </summary>
    void DropDeActive()
    {
        if (countInt > 99)
        {
            textcom.color = Color.clear;
        }
        else
        {
            textcom.text = $"{countInt:00}?";
        }
    }
}
