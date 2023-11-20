using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 슬롯 셀 데이터 정보 코드
/// </summary>
public class SlotCellData : MonoBehaviour
{
    TempSlot temp;
    Image sp;

    bool stackMode = false;
    public bool StackMode
    {
        get
        {
            return stackMode;
        }
        set
        {
            if (stackMode != value)
            {
                stackMode = value;
                if (stackMode)
                {
                    updater += StackModeActive;
                }
                else
                {
                    updater -= StackModeActive;
                }
            }
        }
    }

    public Color BaseColor;
    public Color SetColor;
    public Color CantColor;
    public Color MayColor;

    RectTransform thisRect;

    Action updater;

    /// <summary>
    /// 제조 번호
    /// </summary>
    public int MadeNum;

    /// <summary>
    /// 슬롯에 아이템이 있을때
    /// </summary>
    public bool isSet;

    public bool IsSet
    {
        get
        {
            return isSet;
        }
        set
        {
            if (isSet != value)
            {
                isSet = value;
                if(isSet)
                {

                }
                else
                {

                }
            }
        }
    }

    /// <summary>
    /// 슬롯에 아이템을 두려고 할때
    /// </summary>
    public bool tryToSet;

    /// <summary>
    /// 슬롯에 아이템을 둘 수 있을때
    /// </summary>
    public bool MayToSet;

    private void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        updater = () => { };
        sp = GetComponent<Image>();
    }
    private void Start()
    {
        temp = InventoryInfo.Inst.temp;
        InventoryInfo.Inst.StartOnDrag += () => { StackMode = true; };
        InventoryInfo.Inst.EndDraging -= () => { StackMode = false; };
    }
    private void Update()
    {
        updater();
    }
    void StackModeActive()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(temp.cellRect, transform.position))
        {
            CompareSetable(IsSet);
        }
        else
        {
            DeCompareSetable(IsSet);
        }
    }
    void CompareSetable(bool IsSetNow)
    {
        sp.color = IsSetNow ? CantColor : MayColor;
    }
    void DeCompareSetable(bool IsSetNow)
    {
        sp.color = IsSetNow ? SetColor : BaseColor;
    }
}
