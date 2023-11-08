using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equiped : MonoBehaviour
{
    public List<EquiptBase> EquipMents;
    int nowHold = 0;


    PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
        EquipMents = new List<EquiptBase>(5);
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.MouseAction.performed += UseHold;
        input.Player.MouseAction.canceled += UnUseHold;
    }
    private void OnDisable()
    {
        input.Player.MouseAction.canceled -= UnUseHold;
        input.Player.MouseAction.performed -= UseHold;
        input.Disable();
    }
    private void Start()
    {

    }
    private void UseHold(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (EquipMents[nowHold] != null)
            EquipMents[nowHold].UseAction?.Invoke();
    }
    private void UnUseHold(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (EquipMents[nowHold] != null)
            EquipMents[nowHold].UtillAction?.Invoke();
    }
}
