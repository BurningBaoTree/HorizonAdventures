using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Transform WeaponSlotGroup;
    WeaponSlot[] weaponSlots = new WeaponSlot[3];


    private void Awake()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i] = WeaponSlotGroup.GetChild(i).GetComponent<WeaponSlot>();
        }
    }

}
