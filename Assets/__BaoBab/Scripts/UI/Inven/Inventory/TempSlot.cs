using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum ItemSize
{
    size1x1 = 0,
    size1x2,
    size1x3,
    size2x3,
    size3x3,
    size4x2,
    size4x4,
    size5x2
}

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

    public int[] sizeCoutn;

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

    int copySize;
    int countInt;

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
        invenRect = InventoryInfo.Inst.InventoryRect;
        InventoryInfo.Inst.EndDraging += EndDragTemp;
        FallowActive = true;
        isSucessfulyMoved = false;
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
    /// 템프 슬롯에 정보 삽입
    /// </summary>
    /// <param name="sp">스프라이트 이미지</param>
    /// <param name="valuint">현재 갯수 정보</param>
    /// <param name="max">최대 갯수 정보</param>
    public void LoadInfo(Sprite sp, int valuint, ItemSize size)
    {
        if (sp != null)
        {
            spr.sprite = sp;
            spr.color = Color.white;
            countInt = valuint;
            textcom.text = $"{valuint: 00}";
            textcom.color = Color.white;
            ReSizing(size);
        }
        else
        {
            ResteInfo();
        }

    }
    public void LoadInfo(Sprite sp, ItemSize size)
    {
        if (sp != null)
        {
            spr.sprite = sp;
            spr.color = Color.white;
            textcom.color = Color.clear;
            ReSizing(size);
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
        copySize = sizeCoutn[(int)size];
    }

    void EndDragTemp()
    {
        Dropthis(DropMode);
        PatchThis(InventoryInfo.Inst.slotCellManager.ACCCanStack(copySize));
    }

    public void PatchThis(bool compar)
    {
        if (compar)
        {
            Debug.Log("아이템 슬롯으로 이전");
            isSucessfulyMoved = true;
        }
    }

    public void Dropthis(bool compar)
    {
        if (compar)
        {
            Debug.Log("버림");
            isSucessfulyMoved = true;
        }
    }

    void DropActive()
    {
        textcom.text = "Drop?";
        textcom.color = Color.white;
    }
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
