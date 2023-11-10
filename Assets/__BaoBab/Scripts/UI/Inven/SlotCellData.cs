using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCellData : MonoBehaviour
{
    public Color BaseColor;
    public Color SetColor;
    public Color CantColor;
    public Color MayColor;


    /// <summary>
    /// 제조 번호
    /// </summary>
    public int MadeNum;

    /// <summary>
    /// 슬롯에 아이템이 있을때
    /// </summary>
    public bool IsSet;

    /// <summary>
    /// 슬롯에 아이템을 두려고 할때
    /// </summary>
    public bool tryToSet;


    /// <summary>
    /// 슬롯에 아이템을 둘 수 있을때
    /// </summary>
    public bool MayToSet;


}
