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

    ItemSize size;

    int MaxCount;
    int countInt;

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
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            temp.LoadInfo(spr.sprite, size);
            InventoryInfo.Inst.StartOnDrag?.Invoke();
        }
    }
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
    public void MakeItemInfo(Sprite sprt, int count, ItemSize sz)
    {
        spr.sprite = sprt;
        countInt = count;
        size = sz;
        textcom.text = $"{count :00}";
        spr.color = Color.white;
        textcom.color= Color.white;
        this.gameObject.SetActive(false);
    }
}
