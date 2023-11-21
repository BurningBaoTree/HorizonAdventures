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


    #region 프로퍼티
    /// <summary>
    /// 배치모드 체크용 bool(프로퍼티 있음)
    /// </summary>
    public bool stackMode = false;

    /// <summary>
    /// 배치모드 스위치 bool 프로퍼티
    /// </summary>
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


    /// <summary>
    /// 슬롯에 아이템이 있을때 체크용 bool (프로퍼티 있음)
    /// </summary>
    public bool isSet;

    /// <summary>
    /// 슬롯에 아이템이 있는 경우 bool 프로퍼티
    /// </summary>
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
                if (isSet)
                {
                    sp.color = SetColor;
                    tryToSet = false;
                }
                else
                {
                    sp.color = BaseColor;
                }
            }
        }
    }
    #endregion


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
        InventoryInfo.Inst.EndDraging += () => { StackMode = false; EndDragReset(IsSet); };
    }
    private void Update()
    {
        updater();
    }

    /// <summary>
    /// 배치모드일때 temp의 Rect영역에 이 셀의 중심이 들어가 있을 경우 셀이 배치가 가능한 상태인지 체크하고 아닐 경우 다시 원래대로 복귀
    /// </summary>
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

    /// <summary>
    /// 이 셀이 배치가 가능한 상태인지 체크
    /// </summary>
    /// <param name="IsSetNow"></param>
    void CompareSetable(bool IsSetNow)
    {
        tryToSet = !IsSetNow;
        sp.color = IsSetNow ? CantColor : MayColor;
    }

    /// <summary>
    /// 이 셀이 원상태로 복귀하는 함수
    /// </summary>
    /// <param name="IsSetNow"></param>
    void DeCompareSetable(bool IsSetNow)
    {
        tryToSet = false;
        sp.color = IsSetNow ? SetColor : BaseColor;
    }
    void EndDragReset(bool IsSetNow)
    {
        sp.color = IsSetNow ? SetColor : BaseColor;
    }
}
