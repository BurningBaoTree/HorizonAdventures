using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, UIInventoryController
{
    public Transform WeaponSlotGroup;
    public WeaponSlot[] weaponSlots;

    Action updater;

    int initialcount;

    /// <summary>
    /// 무기슬롯임을 확인하는 bool
    /// </summary>
    public bool isWeaponSlot = false;

    /// <summary>
    /// 아이템임을 확인하는 bool
    /// </summary>
    public bool isItemSlot = false;

    /// <summary>
    /// 상태 슬롯임을 확인하는 bool
    /// </summary>
    public bool isStatusSlot = false;



    #region 프로퍼티

    /// <summary>
    /// 마우스 서치모드인지 확인하는 bool (프로퍼티 있음)
    /// </summary>
    bool mouseSerchMode = false;

    /// <summary>
    /// 마우스 서치 프로퍼티
    /// </summary>
    bool MouseSerchMode
    {
        get
        {
            return mouseSerchMode;
        }
        set
        {
            mouseSerchMode = value;
            if (mouseSerchMode)
            {
                updater += SerchActive;
            }
            else
            {
                updater -= SerchActive;
            }
        }
    }
    #endregion


    private void Awake()
    {
        weaponSlots = new WeaponSlot[WeaponSlotGroup.childCount];
        for (int i = 0; i < WeaponSlotGroup.childCount; i++)
        {
            weaponSlots[i] = WeaponSlotGroup.GetChild(i).GetComponent<WeaponSlot>();
        }
        initialcount = 0;
        updater = () => { };
    }

    private void OnEnable()
    {
        if (initialcount > 0)
        {
            ResetTheWeaponSlot();
            MouseSerchMode = true;
        }
        initialcount++;
    }
    private void OnDisable()
    {
        MouseSerchMode = false;
    }

    private void Update()
    {
        updater();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        /*        originalPanelLocalPosition = panelRectTransform.localPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*        if (panelRectTransform != null)
                {
                    Vector2 localPointerPosition;
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
                    {
                        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                        panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
                    }
                }*/
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

    /// <summary>
    /// 마우스가 무슨 활동을 할건지 Ray를 쏘는 함수
    /// </summary>
    void SerchActive()
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero,Mathf.Infinity,5);
        if (hit.collider.gameObject != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log(hitObject);
            compairTheUI(hitObject);
        }
    }

    void compairTheUI(GameObject obj)
    {
        if (obj.GetComponent<WeaponSlot>() != null)
        {
            isWeaponSlot = true;
            Debug.Log("무기슬롯임");
        }
        if (obj.GetComponent<StatusInfo>() != null)
        {
            isStatusSlot = true;
            Debug.Log("스탯사항입니다.");
        }
        /*        if (obj.GetComponent<WeaponSlot>() != null)
                {
                    isWeaponSlot = true;
                    Debug.Log("무기슬롯임");
                }*/
    }
}
