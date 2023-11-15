using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 인벤토리 창 게임 오브젝트
    /// </summary>
    GameObject InventoryPanel;

    /// <summary>
    /// 맵 창 게임 오브젝트
    /// </summary>
    GameObject MapPanel;



    PlayerInput input;


    #region 프로퍼티

    /// <summary>
    /// 인벤토리 창을 켜주는 bool(프로퍼티 있음)
    /// </summary>
    bool inventoryOpen = true;

    /// <summary>
    /// 인벤토리 창을 켜주는 프로퍼티
    /// </summary>
    bool InventoyOpen
    {
        get
        {
            return inventoryOpen;
        }
        set
        {
            inventoryOpen = value;
            InventoryPanel.SetActive(inventoryOpen);
            MouseActive = !inventoryOpen;
        }
    }

    /// <summary>
    /// 마우스 On Off 용 bool 변수(프로퍼티 있음)
    /// </summary>
    public bool mouseActive = true;

    /// <summary>
    /// 마우스를 십자로 둘것인지 풀어줄것인지 선택하는 프로퍼티(참일때 잠김)
    /// </summary>
    public bool MouseActive
    {
        get
        {
            return mouseActive;
        }
        set
        {
            mouseActive = value;
            Cursor.lockState = mouseActive ? CursorLockMode.Locked : CursorLockMode.None;
            GameManager.Inst.PlayerEquiped.MouseCross.gameObject.SetActive(value);
        }
    }

    #endregion
    private void Awake()
    {
        InventoryPanel = transform.GetChild(0).gameObject;
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.UI.Inventory.performed += OpenIventory;
    }
    private void Start()
    {
        MouseActive = true;
        InventoyOpen = !InventoyOpen;
    }

    /// <summary>
    /// 인벤토리 창 버튼 입력
    /// </summary>
    /// <param name="obj"></param>
    private void OpenIventory(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        InventoyOpen = !InventoyOpen;
    }
}

