using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 인벤토리의 정보를 관리하는 코드
/// </summary>
public class InventoryInfo : MonoBehaviour
{
    /// <summary>
    /// 접근용 변수
    /// </summary>
    static InventoryInfo instance;
    public static InventoryInfo Inst => instance;

    /// <summary>
    /// 드래그를 시작했을때 실행되는 델리게이트
    /// </summary>
    public Action StartOnDrag;

    /// <summary>
    /// 드래그 중일때 실행되는 델리게이트
    /// </summary>
    public Action OnDraging;

    /// <summary>
    /// 드래그가 끝났을때 실행되는 델리게이트
    /// </summary>
    public Action EndDraging;

    /// <summary>
    /// 무기슬롯 리스트가 바뀌었을때 호출
    /// </summary>
    public Action ListHasBeenChanged;

    /// <summary>
    /// 템프슬롯
    /// </summary>
    public TempSlot temp;

    /// <summary>
    /// 무기 슬롯의 부모 개체
    /// </summary>
    Transform WeaponSlotGroup;

    /// <summary>
    /// 무기 슬롯 배열
    /// </summary>
    WeaponSlot[] weaponSlots;

    /// <summary>
    /// 무기 / 장비 설명 칸
    /// </summary>
    TextMeshProUGUI descriptionSlot;

    public RectTransform InventoryRect;

    public SlotCellManager slotCellManager;

    public BagManager BagParent;

    public SubWeaponBase subslot;
    public EquiptBase[] equipinven = new EquiptBase[3];

    /// <summary>
    /// 초기화용 int
    /// </summary>
    int initialcount;

    #region 프로퍼티

    #endregion


    private void Awake()
    {
        InventoryRect = GetComponent<RectTransform>();
        instance = this;
        WeaponSlotGroup = transform.GetChild(0).GetChild(4);
        descriptionSlot = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        weaponSlots = new WeaponSlot[WeaponSlotGroup.childCount];
        BagParent = transform.GetChild(0).GetChild(7).GetComponent<BagManager>();
        slotCellManager = transform.GetChild(0).GetChild(0).GetComponent<SlotCellManager>();
        for (int i = 0; i < WeaponSlotGroup.childCount; i++)
        {
            weaponSlots[i] = WeaponSlotGroup.GetChild(i).GetComponent<WeaponSlot>();
        }
        initialcount = 0;
    }

    private void OnEnable()
    {
        if (initialcount > 0)
        {
            InitializeInventory();
        }
        initialcount++;
    }

    /// <summary>
    /// 인벤토리 초기화
    /// </summary>
    void InitializeInventory()
    {
        ResetTheWeaponSlot();
        DisplayDescription(null, null);
    }

    /// <summary>
    /// 플레이어의 장비창을 보고 리스트 리셋
    /// </summary>
    public void ResetTheWeaponSlot()
    {
        if (GameManager.Inst.PlayerEquiped.subweapon != null)
        {
            SubWeaponBase sub = GameManager.Inst.PlayerEquiped.subweapon;
            subslot = sub;
            weaponSlots[0].initializeWeaponSlot(sub.temData);
        }
        else
        {
            subslot = null;
            weaponSlots[0].initializeWeaponSlot(null);
        }
        for (int i = 1; i < WeaponSlotGroup.childCount; i++)
        {
            if (GameManager.Inst.PlayerEquiped.Equipments[i - 1] != null)
            {
                EquiptBase equipt = GameManager.Inst.PlayerEquiped.Equipments[i - 1];
                equipinven[i-1] = equipt;
                weaponSlots[i].initializeWeaponSlot(equipt.temData);
            }
            else
            {
                equipinven[i-1] = null;
                weaponSlots[i].initializeWeaponSlot(null);
            }
        }
    }

    /// <summary>
    /// 설명창 재갱신
    /// </summary>
    /// <param name="NameOf">이름</param>
    /// <param name="descript">설명</param>
    public void DisplayDescription(string NameOf, string descript)
    {
        if (NameOf != null)
        {
            descriptionSlot.color = Color.white;
            descriptionSlot.text = $"<size=35><b>{NameOf}</b>:</size> <size=30>{descript}</size> ";
        }
        else
        {
            descriptionSlot.color = Color.clear;
            descriptionSlot.text = null;
        }
    }
}
