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

    public RectTransform WeaponSlotRect;

    ItemData WeaponData;

    /// <summary>
    /// 비어있는지 체크용 bool
    /// </summary>
    bool isEmpty = true;

    public bool isSubSlot;

    /// <summary>
    /// 무기 이미지
    /// </summary>
    public Image weaponImage;

    /// <summary>
    /// 이름 텍스트메쉬
    /// </summary>
    public TextMeshProUGUI NameText;

    /// <summary>
    /// 잔여 갯수 텍스트 메쉬
    /// </summary>
    public TextMeshProUGUI LeftBullet;

    /// <summary>
    /// 사이즈 열거 변수
    /// </summary>
    ItemSize size;

    /// <summary>
    /// 이름 문자변수
    /// </summary>
    string NameOfWeapon;

    /// <summary>
    /// 설명 문자 변수
    /// </summary>
    string DescriptionOfWeapon;

    private void Awake()
    {
        weaponImage = transform.GetChild(0).GetComponent<Image>();
        NameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        LeftBullet = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        WeaponSlotRect = GetComponent<RectTransform>();
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
    public void initializeWeaponSlot(ItemData weaponData)
    {
        if (weaponData == null)
        {
            isEmpty = true;
            weaponImage.sprite = null;
            weaponImage.color = Color.clear;

            NameText.color = Color.clear;
            LeftBullet.color = Color.clear;
            LeftBullet.text = null;
        }
        else
        {
            isEmpty = false;
            weaponImage.sprite = weaponData.itemIcon;
            weaponImage.color = Color.white;
            this.size = weaponData.size;

            NameText.color = Color.white;
            NameText.text = weaponData.itemName;
            NameOfWeapon = weaponData.itemName;
            DescriptionOfWeapon = weaponData.itemDescription;
        }
/*
        if (weaponData.maxStackCount == 1)
        {
            LeftBullet.color = Color.clear;
        }
        else
        {
            LeftBullet.color = Color.white;
            LeftBullet.text = $"{NowState: 000} / {MaxState: 000}";
        }*/
    }

    /// <summary>
    /// 무기 슬롯에 마우스 포인터를 올리면 설명이 보이게 한다.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            InventoryInfo.Inst.DisplayDescription(NameOfWeapon, DescriptionOfWeapon);
        }
    }

    /// <summary>
    /// 무기 슬롯에 마우스 포인터를 빼면 설명이 안보이게 한다.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            InventoryInfo.Inst.DisplayDescription(null, null);
        }
    }

    /// <summary>
    /// 마우스 클릭시 실행되는 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            InventoryInfo.Inst.StartOnDrag?.Invoke();
            temp.gameObject.SetActive(true);
            temp.LoadInfo(weaponImage.sprite, size);
            invisival();
        }
    }
    /// <summary>
    /// 드래그 시작 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            base.OnBeginDrag(eventData);
            temp.gameObject.SetActive(true);
            temp.LoadInfo(weaponImage.sprite, size);
            invisival();
        }
    }

    /// <summary>
    /// 드래그 종료 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            base.OnEndDrag(eventData);
            temp.ResteInfo();
            temp.gameObject.SetActive(false);
            if (temp.isSucessfulyMoved)
            {
                if(RectTransformUtility.RectangleContainsScreenPoint(WeaponSlotRect,temp.transform.position))
                {

                }
                ResetSlot();
                InventoryInfo.Inst.ListHasBeenChanged?.Invoke();
            }
            else
            {
                visival();
            }
        }
    }

    /// <summary>
    /// 무기슬롯 투명화
    /// </summary>
    void invisival()
    {
        weaponImage.color = Color.clear;
        NameText.color = Color.clear;
        LeftBullet.color = Color.clear;
    }

    /// <summary>
    /// 무기슬롯 다시 보이기
    /// </summary>
    void visival()
    {
        weaponImage.color = Color.white;
        NameText.color = Color.white;
        LeftBullet.color = Color.white;
    }

    /// <summary>
    /// 무기슬롯 초기화
    /// </summary>
    void ResetSlot()
    {
        weaponImage.color = Color.clear;
        weaponImage.sprite = null;
        NameText.color = Color.clear;
        NameText.text = null;
        LeftBullet.color = Color.clear;
        LeftBullet.text = null;
        isEmpty = true;
    }
}
