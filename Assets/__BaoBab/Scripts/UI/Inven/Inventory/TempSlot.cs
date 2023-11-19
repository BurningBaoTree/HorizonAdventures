using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 템프 슬롯 코드
/// </summary>
public class TempSlot : InventoryCon
{

    Action updater;

    /// <summary>
    /// 스프라이트 렌더러 이미지
    /// </summary>
    SpriteRenderer spr;

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
        temp = this;
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spr.color = Color.clear;
        textcom = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textcom.color = Color.clear;
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
    /// <param name="sp">스프라이트</param>
    /// <param name="textInfo">텍스트 내용</param>
    protected void LoadInfo(Sprite sp, int max, int valuint)
    {
        spr.sprite = sp;
        spr.color = Color.white;
        textcom.text = $"{valuint: 00} / {max: 00}";
        textcom.color = Color.white;
    }

    /// <summary>
    /// 템프슬롯 초기화
    /// </summary>
    protected void ResteInfo()
    {
        spr.sprite = null;
        spr.color = Color.clear;
        textcom.text = null;
        textcom.color = Color.clear;
    }
}
