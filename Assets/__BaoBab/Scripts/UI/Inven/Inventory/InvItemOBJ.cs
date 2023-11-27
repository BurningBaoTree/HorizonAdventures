using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 내부에서 사용될 아이템 오브젝트
/// </summary>
public class InvItemOBJ : InventoryCon
{
    TempSlot temp;

    /// <summary>
    /// 이미지의 Rect
    /// </summary>
    RectTransform sprRect;

    /// <summary>
    /// 셀의 Rect
    /// </summary>
    RectTransform cellRect;

    /// <summary>
    /// 스프라이트 렌더러 이미지
    /// </summary>
    Image spr;

    /// <summary>
    /// 물량을 뵤여주는 TextMash
    /// </summary>
    TextMeshProUGUI textcom;

    ItemData itemData;

    public SubWeaponBase subwp;
    public EquiptBase mainwp;

    uint MaxCount;
    uint countInt;

    public List<SlotCellData> cellOnIt = new List<SlotCellData>();

    private void Awake()
    {
        spr = transform.GetChild(0).GetComponent<Image>();
        sprRect = spr.GetComponent<RectTransform>();
        cellRect = transform.GetChild(1).GetComponent<RectTransform>();
        spr.color = Color.clear;
        textcom = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        textcom.color = Color.clear;
    }
    private void OnEnable()
    {
        temp = InventoryInfo.Inst.temp;
    }

    /// <summary>
    /// 드래그가 시작될때
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            invisival();
            CellEmpty();
            if (subwp != null)
            {
                temp.LoadInfo(subwp);
            }
            else if (mainwp != null)
            {
                temp.LoadInfo(mainwp);
            }
            else
            {
                temp.LoadInfo(itemData, countInt);
            }
            InventoryInfo.Inst.StartOnDrag?.Invoke();
        }
    }

    /// <summary>
    /// 드래그 끝났을때
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (temp != null)
        {
            if (temp.IsSucessfulyMoved)
            {
                Destroy(this.gameObject);
            }
            else
            {
                CellBackFill();
                visival();
            }
            temp.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 정보 받아오기
    /// </summary>
    /// <param name="sprt">스프라이트</param>
    /// <param name="count">갯수</param>
    /// <param name="sz">사이즈</param>
    public void MakeItemInfo(ItemData tem, uint count)
    {
        itemData = tem;
        spr.sprite = tem.itemIcon;
        countInt = count;
        MaxCount = tem.maxStackCount;
        ReSizing(tem.size);
        textcom.text = $"{count:00} / {tem.maxStackCount:00}";
        spr.color = Color.white;
        textcom.color = Color.white;
        this.gameObject.SetActive(true);
    }
    public void MakeItemInfo(SubWeaponBase tem, uint count)
    {
        MakeItemInfo(tem.temData, count);
        subwp = tem;
    }
    public void MakeItemInfo(EquiptBase tem, uint count)
    {
        MakeItemInfo(tem.temData, count);
        mainwp = tem;
    }

    /// <summary>
    /// 사이즈 고치기
    /// </summary>
    /// <param name="size"></param>
    void ReSizing(ItemSize size)
    {
        sprRect.sizeDelta = temp.ImageSizeList[(int)size];
        cellRect.sizeDelta = temp.CellsizeList[(int)size];
    }

    /// <summary>
    /// 무기슬롯 투명화
    /// </summary>
    void invisival()
    {
        spr.color = Color.clear;
        textcom.color = Color.clear;
        cellRect.gameObject.SetActive(false);
    }

    /// <summary>
    /// 무기슬롯 다시 보이기
    /// </summary>
    void visival()
    {
        spr.color = Color.white;
        textcom.color = Color.white;
        cellRect.gameObject.SetActive(true);
    }

    /// <summary>
    /// 셀의 set을 비활성화
    /// </summary>
    void CellEmpty()
    {
        foreach (SlotCellData cell in cellOnIt)
        {
            cell.IsSet = false;
        }
    }

    /// <summary>
    /// 셀의 set을 활성화
    /// </summary>
    void CellBackFill()
    {
        foreach (SlotCellData cell in cellOnIt)
        {
            cell.IsSet = true;
        }
    }

    /// <summary>
    /// 이 아이템에 같은 종류의 아이템 갯수를 추가한다.
    /// </summary>
    public void StackItem()
    {

    }
}
