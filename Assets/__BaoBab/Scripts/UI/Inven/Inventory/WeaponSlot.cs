using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 무기슬롯 코드
/// </summary>
public class WeaponSlot : InventoryCon
{
    TempSlot temp;

    bool isEmpty = true;

    public Image weaponImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LeftBullet;

    ItemSize size;
    string NameOfWeapon;
    string DescriptionOfWeapon;

    private void Awake()
    {
        weaponImage = transform.GetChild(0).GetComponent<Image>();
        NameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        LeftBullet = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        temp = InventoryInfo.Inst.temp;
    }
    /// <summary>
    /// 무기 슬롯 동기화
    /// </summary>
    /// <param name="imageWP"></param>
    /// <param name="NameWP"></param>
    /// <param name="NowState"></param>
    /// <param name="MaxState"></param>
    public void initializeWeaponSlot(Sprite imageWP, string NameWP, int NowState, int MaxState, string Des, ItemSize size)
    {
        if (imageWP == null)
        {
            isEmpty = true;
            weaponImage.sprite = null;
            weaponImage.color = Color.clear;
        }
        else
        {
            isEmpty = false;
            weaponImage.sprite = imageWP;
            weaponImage.color = Color.white;
            this.size = size;
        }

        if (NameWP == null)
        {
            NameText.color = Color.clear;
        }
        else
        {
            NameText.color = Color.white;
            NameText.text = NameWP;
            NameOfWeapon = NameWP;
            DescriptionOfWeapon = Des;
        }

        if (MaxState == 0)
        {
            LeftBullet.color = Color.clear;
        }
        else
        {
            LeftBullet.color = Color.white;
            LeftBullet.text = $"{NowState: 000} / {MaxState: 000}";
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            InventoryInfo.Inst.DisplayDescription(NameOfWeapon, DescriptionOfWeapon);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            InventoryInfo.Inst.DisplayDescription(null, null);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("누름");
    }

    /// <summary>
    /// 드래그 시작 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            temp.LoadInfo(weaponImage.sprite, size);
            InventoryInfo.Inst.StartOnDrag?.Invoke();
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 중");
    }

    /// <summary>
    /// 드래그 종료 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.ResteInfo();
            temp.gameObject.SetActive(false);
            InventoryInfo.Inst.EndDraging?.Invoke();
        }
    }
}
