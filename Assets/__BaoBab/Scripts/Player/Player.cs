using System;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Player : MonoBehaviour
{
    [Header("스테이터스")]
    public int hp;
    public int damage;

    [Header("이동 관련")]
    public float MoveSpeed;
    public float JumpPower;
    public float MaxJump;

    bool inRunning = false;
    bool inJump = false;
    bool inAir = false;
    public bool inLadder = false;

    Vector2 dir = Vector2.zero;

    Rigidbody2D rb;
    PlayerInput input;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D CC2D;
    CoolTimeSys cooltimer;

    Action fixedAction;
    Action action;

    #region 프로퍼티
    public bool InRunning
    {
        get
        {
            return inRunning;
        }
        set
        {
            if (inRunning != value)
            {
                if (dir.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (dir.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                inRunning = value;
                if (inRunning)
                {
                    animator.SetBool("Run", true);
                    fixedAction += moveActive;
                }
                else
                {
                    animator.SetBool("Run", false);
                    fixedAction -= moveActive;
                }
            }
        }
    }
    public bool InJump
    {
        get
        {
            return inJump;
        }
        set
        {
            if (inJump != value)
            {
                inJump = value;
                if (inJump)
                {
                    rb.AddForce(JumpPower * Vector2.up, ForceMode2D.Impulse);
                    action += JumpActive;
                    animator.SetInteger("Jump", 1);
                }
                else
                {
                    animator.SetInteger("Jump", 0);
                    action -= JumpActive;
                }
            }
        }
    }
    public bool InAir
    {
        get
        {
            return inAir;
        }
        set
        {
            if (inAir != value)
            {
                inAir = value;
                if (inAir && !InJump)
                {
                    animator.SetInteger("Jump", 3);
                }
                else if (!inAir)
                {
                    animator.SetInteger("Jump", 0);
                }
            }
        }
    }
    public bool InLadder
    {
        get
        {
            return inLadder;
        }
        set
        {
            if (inLadder != value)
            {
                inLadder = value;
                if(!inLadder)
                {
                    CC2D.isTrigger = false;
                    fixedAction -= LadderActive;
                }
            }
        }
    }
    #endregion

    private void Awake()
    {
        cooltimer = GetComponent<CoolTimeSys>();
        CC2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        input = new PlayerInput();
        spriteRenderer = GetComponent<SpriteRenderer>();
        action += () => { };
        fixedAction = () => { };
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        input.Player.Jump.performed -= OnJump;
        input.Player.Move.canceled -= OnMove;
        input.Player.Move.performed -= OnMove;
        input.Disable();
    }
    private void Update()
    {
        action();
    }
    private void FixedUpdate()
    {
        fixedAction();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            InJump = false;
            InAir = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder") && dir.y != 0)
        {
            InLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            InLadder = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            InAir = true;
        }
    }
    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        dir = obj.ReadValue<Vector2>();
        if (!InLadder)
        {
            InRunning = true;
        }
        else
        {
            InRunning = false;
            if (dir.y != 0)
            {
                CC2D.isTrigger = true;
                fixedAction += LadderActive;
            }
        }
        if (dir.x == 0)
        {
            InRunning = false;
        }
    }
    void moveActive()
    {
        transform.Translate(MoveSpeed * Time.fixedDeltaTime * dir * Vector2.right);
    }
    void LadderActive()
    {
        transform.Translate(MoveSpeed * Time.fixedDeltaTime * dir * Vector2.up);
    }
    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!InAir)
        {
            InJump = true;
            InLadder = false;
        }
    }
    void JumpActive()
    {
        if (rb.velocity.y < 0.8f)
        {
            animator.SetInteger("Jump", 2);
            action -= JumpActive;
        }
    }
}
