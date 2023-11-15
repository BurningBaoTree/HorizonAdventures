using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform WeaponSlotGroup;
    public WeaponSlot[] weaponSlots;

    int initialcount;


    private void Awake()
    {
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

    /// <summary>
    /// 플레이어의 장비창을 보고 리스트 리셋
    /// </summary>
    private void ResetTheWeaponSlot()
    {
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
