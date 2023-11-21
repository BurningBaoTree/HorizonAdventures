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


    /// <summary>
    /// 비어있는지 체크용 bool
    /// </summary>
    bool isEmpty = true;

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
    /// 무기 슬롯에 마우스 포인터를 올리면 설명이 안보이게 한다.
    /// </summary>
    /// <param name="eventData"></param>
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
        if (!isEmpty)
        {
            if (temp != null)
            {
                temp.gameObject.SetActive(true);
                temp.LoadInfo(weaponImage.sprite, size);
                InventoryInfo.Inst.StartOnDrag?.Invoke();
                invisival();
            } 
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
        if (!isEmpty)
        {
            if (temp != null)
            {
                temp.ResteInfo();
                InventoryInfo.Inst.EndDraging?.Invoke();
                temp.gameObject.SetActive(false);
            }
            if (temp.isSucessfulyMoved)
            {
                ResetSlot();
            }
            else
            {
                visival();
            } 
        }
    }

    void invisival()
    {
        weaponImage.color = Color.clear;
        NameText.color = Color.clear;
        LeftBullet.color = Color.clear;
    }
    void visival()
    {
        weaponImage.color = Color.white;
        NameText.color = Color.white;
        LeftBullet.color = Color.white;
    }
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
