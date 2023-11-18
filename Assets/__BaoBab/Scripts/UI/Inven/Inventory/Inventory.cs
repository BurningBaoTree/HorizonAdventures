using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 인벤토리의 정보를 관리하는 코드
/// </summary>
public class Inventory : MonoBehaviour
{

    Inventory instance;
    public Inventory Inst => instance;

    /// <summary>
    /// 무기 슬롯의 부모 개체
    /// </summary>
    Transform WeaponSlotGroup;

    /// <summary>
    /// 무기 슬롯 배열
    /// </summary>
    WeaponSlot[] weaponSlots;

    TextMeshProUGUI descriptionSlot;


    /// <summary>
    /// 초기화용 int
    /// </summary>
    int initialcount;

    #region 프로퍼티

    #endregion


    private void Awake()
    {
        instance = this;
        WeaponSlotGroup = transform.GetChild(0).GetChild(4);
        descriptionSlot = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        weaponSlots = new WeaponSlot[WeaponSlotGroup.childCount];
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
            ResetTheWeaponSlot();
        }
        initialcount++;
    }
    private void OnDisable()
    {

    }

    /// <summary>
    /// 플레이어의 장비창을 보고 리스트 리셋
    /// </summary>
    private void ResetTheWeaponSlot()
    {
        if (GameManager.Inst.PlayerEquiped.subweapon != null)
        {
            SubWeaponBase sub = GameManager.Inst.PlayerEquiped.subweapon;
            weaponSlots[0].initializeWeaponSlot(sub.subSpRender.sprite, sub.weaponName, 0, 0);
        }
        else
        {
            weaponSlots[0].initializeWeaponSlot(null, null, 0, 0);
        }
        for (int i = 1; i < WeaponSlotGroup.childCount; i++)
        {
            if (GameManager.Inst.PlayerEquiped.Equipments[i - 1] != null)
            {
                EquiptBase equipt = GameManager.Inst.PlayerEquiped.Equipments[i - 1];
                weaponSlots[i].initializeWeaponSlot(equipt.spRender.sprite, equipt.weaponName, equipt.ammoCount, 999);
            }
            else
            {
                weaponSlots[i].initializeWeaponSlot(null, null, 0, 0);
            }
        }
    }
}
