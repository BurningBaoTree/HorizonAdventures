using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    uint MaxCount;
    uint countInt;

    private void Awake()
    {
        spr = transform.GetChild(0).GetComponent<Image>();
        sprRect = spr.GetComponent<RectTransform>();
        cellRect = transform.GetChild(1).GetComponent<RectTransform>();
        spr.color = Color.clear;
        textcom = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        textcom.color = Color.clear;
    }
    private void Start()
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
            temp.LoadInfo(itemData, countInt);
            InventoryInfo.Inst.StartOnDrag?.Invoke();
        }
    }

    /// <summary>
    /// 드래그 끝났을때
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.ResteInfo();
            InventoryInfo.Inst.EndDraging?.Invoke();
            temp.gameObject.SetActive(false);
        }
        if(temp.isSucessfulyMoved)
        {
            Destroy(this);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 아이템 정보 받아오기
    /// </summary>
    /// <param name="sprt">스프라이트</param>
    /// <param name="count">갯수</param>
    /// <param name="sz">사이즈</param>
    public void MakeItemInfo(ItemData tem , uint count)
    {
        itemData = tem;
        spr.sprite = tem.itemIcon;
        countInt = count;
        MaxCount = tem.maxStackCount;
        textcom.text = $"{count :00}";
        spr.color = Color.white;
        textcom.color= Color.white;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 이 아이템에 같은 종류의 아이템 갯수를 추가한다.
    /// </summary>
    public void StackItem()
    {

    }
}
