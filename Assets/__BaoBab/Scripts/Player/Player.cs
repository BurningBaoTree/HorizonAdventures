using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float MoveSpeed;
    public float JumpPower;

    bool InAir = true;

    PlayerInput input;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += Player_Move;
        input.Player.Move.canceled += Player_Move;

        input.Player.Jump.performed += Player_Jump;
    }

    private void Player_Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {

    }
    private void Player_Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {

    }
}
