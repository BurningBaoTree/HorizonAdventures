using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 인벤토리 컨트롤러 부모 클래스
/// </summary>
public class InventoryCon : MonoBehaviour, UIInventoryController
{
    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        InventoryInfo.Inst.StartOnDrag?.Invoke();
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        InventoryInfo.Inst.OnDraging?.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        InventoryInfo.Inst.EndDraging?.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }
}
