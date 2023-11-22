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
    /// 현재 들고있는 아이템 데이터
    /// </summary>
    public ItemData copyTemInfo;

    /// <summary>
    /// 현재 갯수
    /// </summary>
    public uint countInt;

    /// <summary>
    /// 인벤토리 Rect사이즈
    /// </summary>
    RectTransform invenRect;

    public bool isSucessfulyMoved = false;

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
        copyTemInfo = null;
        invenRect = InventoryInfo.Inst.InventoryRect;
        FallowActive = true;
        isSucessfulyMoved = false;
    }
    private void Start()
    {
        InventoryInfo.Inst.EndDraging += EndDragTemp;
    }
    private void OnDisable()
    {
        FallowActive = false;
        countInt = 999;
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
    /// <param name="valuint">아이템 갯수</param>
    public void LoadInfo(ItemData tem, uint valuint)
    {
        if (tem != null)
        {
            copyTemInfo = tem;
            spr.sprite = tem.itemIcon;
            spr.color = Color.white;
            countInt = valuint;
            textcom.text = $"{valuint: 00}";
            textcom.color = Color.white;
            ReSizing(tem.size);
        }
        else
        {
            ResteInfo();
        }

    }
    public void LoadInfo(ItemData tem)
    {
        if (tem != null)
        {
            copyTemInfo = tem;
            spr.sprite = tem.itemIcon;
            spr.color = Color.white;
            textcom.color = Color.clear;
            ReSizing(tem.size);
        }
        else
        {
            ResteInfo();
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
        //인벤토리 창으로 패치가 가능하면 패치한다.
        PatchThis(InventoryInfo.Inst.slotCellManager.ACCCanStack(copyTemInfo));
    }

    /// <summary>
    /// 배치가 가능하면 배치하는 함수
    /// </summary>
    /// <param name="compar"></param>
    public void PatchThis(bool compar)
    {
        if (compar)
        {
            isSucessfulyMoved = true;
            Debug.Log("아이템 슬롯으로 이전");
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
            isSucessfulyMoved = true;
            Debug.Log("버림");
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
