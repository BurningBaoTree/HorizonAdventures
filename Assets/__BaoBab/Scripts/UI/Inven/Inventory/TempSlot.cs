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

    public Vector2[] CellsizeList;
    public Vector2[] ImageSizeList;

    RectTransform sprRect;
    RectTransform cellRect;

    /// <summary>
    /// 스프라이트 렌더러 이미지
    /// </summary>
    Image spr;

    TextMeshProUGUI textcom;

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
                    updater += FollowCursor;
                }
            }
        }
    }


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
        Debug.Log("활성");
        FallowActive = true;
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
    /// temp슬롯 마우스 따라가는 함수
    /// </summary>
    void FollowCursor()
    {
        transform.position = Mouse.current.position.ReadValue();
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
    void ReSizing(ItemSize size)
    {
        sprRect.sizeDelta = ImageSizeList[(int)size];
        cellRect.sizeDelta= CellsizeList[(int)size];
    }
}
