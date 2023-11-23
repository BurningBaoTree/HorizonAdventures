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

    ItemData WeaponInfo;

    /// <summary>
    /// 비어있는지 체크용 bool
    /// </summary>
    bool isEmpty = true;

    public bool isSubSlot;

    public int Madenumber;

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


    public SubWeaponBase subWeapon;
    public EquiptBase equiptGear;

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
    public void subinitializeWeaponSlot(SubWeaponBase weapon)
    {
        if (weapon == null)
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
            subWeapon = weapon;
            WeaponInfo = weapon.temData;
            isEmpty = false;
            weaponImage.sprite = weapon.temData.itemIcon;
            weaponImage.color = Color.white;
            this.size = weapon.temData.size;

            NameText.color = Color.white;
            NameText.text = weapon.temData.itemName;
            NameOfWeapon = weapon.temData.itemName;
            DescriptionOfWeapon = weapon.temData.itemDescription;
        }
    }
    public void initializeWeaponSlot(EquiptBase weapon)
    {
        if (weapon == null)
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
            equiptGear =weapon;
            WeaponInfo = weapon.temData;
            isEmpty = false;
            weaponImage.sprite = weapon.temData.itemIcon;
            weaponImage.color = Color.white;
            this.size = weapon.temData.size;

            NameText.color = Color.white;
            NameText.text = weapon.temData.itemName;
            NameOfWeapon = weapon.temData.itemName;
            DescriptionOfWeapon = weapon.temData.itemDescription;
        }
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
    /// 드래그 시작 함수
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            base.OnBeginDrag(eventData);
            temp.gameObject.SetActive(true);
            if (isSubSlot)
            {
                temp.LoadInfo(subWeapon);
            }
            else
            {
                temp.LoadInfo(equiptGear);
            }
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
                //성공적으로 이동했는데 위치가 이 무기슬롯일대
                if (RectTransformUtility.RectangleContainsScreenPoint(WeaponSlotRect, temp.transform.position))
                {
                    if (isSubSlot)
                    {
                        InventoryInfo.Inst.subslot = temp.copySubWeaponData;
                    }
                    else
                    {
                        InventoryInfo.Inst.equipinven[Madenumber] = temp.copyWeaponData;
                    }
                }
/*                if (isSubSlot)
                {
                    InventoryInfo.Inst.subslot = temp.
                    }
                else
                {
                    InventoryInfo.Inst.equipinven[Madenumber] =
                    }*/
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
        WeaponInfo = null;
    }
}
