using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 플레이어 이동 코드(이동 속도, 이동 상태와 같은 코드는 이곳에)
/// </summary>
public class Player_Move : MonoBehaviour
{
    /// <summary>
    /// 이동 속도 , 점프 파워
    /// </summary>
    [Header("이동 관련")]
    public float MoveSpeed;
    public float JumpPower;

    /// <summary>
    /// 이동속도 카피 변수
    /// </summary>
    float copySpeed;

    /// <summary>
    /// 달리기 bool
    /// </summary>
    public bool inRunning = false;

    /// <summary>
    /// 점프 bool 
    /// </summary>
    public bool inJump = false;

    /// <summary>
    /// 공중 체크 bool
    /// </summary>
    public bool inAir = false;

    /// <summary>
    /// 사다리 발견 체크 bool
    /// </summary>
    public bool inLadder = false;

    /// <summary>
    /// 사다리 탑슴 bool
    /// </summary>
    public bool ladderRide = false;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector2 dir = Vector2.zero;

    Rigidbody2D rb;
    PlayerInput input;
    Animator animator;
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// FixedUpdate 델리게이트
    /// </summary>
    Action fixedAction;

    /// <summary>
    /// Update 델리게이트
    /// </summary>
    Action action;

    #region 프로퍼티

    /// <summary>
    /// 이동 작업 프로퍼티
    /// </summary>
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
                ///이동 방향에 따라 스프라이트 뒤집기
                if (dir.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (dir.x > 0)
                {
                    spriteRenderer.flipX = false;
                }

                inRunning = value;

                ///달리기 애니메이터 실행, 그리고 이동함수 fixedUpdate에 구독
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

    /// <summary>
    /// 점프 작업 프로퍼티
    /// </summary>
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

    /// <summary>
    /// 공중에 떠있을때 프로퍼티
    /// </summary>
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

    /// <summary>
    /// 사다리에 접근했을때 프로퍼티
    /// </summary>
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
                if (!inLadder)
                {
                    LadderRideing = false;
                }
            }
        }
    }

    /// <summary>
    /// 사다리에 타고있을때 프로퍼티
    /// </summary>
    public bool LadderRideing
    {
        get
        {
            return ladderRide;
        }
        set
        {
            if (ladderRide != value)
            {
                ladderRide = value;
                ///사다리 타기 상태일때
                if (ladderRide)
                {
                    //위치 고정
                    Vector2 startpos = ((Vector2)transform.position) * Vector2.one;
                    startpos.x = Mathf.Floor(transform.position.x) + 0.5f;
                    transform.position = startpos;

                    //물리값과 중력값 0
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;

                    //애니메이터와 속도, 그리고 사다리 타기 움직임 활성화
                    animator.SetBool("LadderRide", true);
                    MoveSpeed = MoveSpeed * 0.5f;
                    fixedAction += LadderActive;
                }
                ///사다리 타기 상태가 아닐때
                else
                {
                    //움직임 비활성화 애니메이션 초기화
                    fixedAction -= LadderActive;
                    animator.SetBool("LadderRide", false);
                    animator.SetFloat("Ladder", 0f);

                    //중력 다시 적용 속도 초기화
                    rb.gravityScale = 1;
                    MoveSpeed = copySpeed;

                    //dir이 0이 아니면(달리기를 하려고 하는중)
                    if (dir != Vector2.zero)
                    {
                        rb.AddForce(dir * 2f, ForceMode2D.Impulse);
                        InRunning = true;
                    }
                }
            }
        }
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        input = new PlayerInput();
        spriteRenderer = GetComponent<SpriteRenderer>();
        copySpeed = MoveSpeed;
    }
    private void OnEnable()
    {
        playerMoveInitialize();
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
            LadderRideing = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            InAir = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
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

    /// <summary>
    /// 이동 관련 초기화
    /// </summary>
    void playerMoveInitialize()
    {
        action = () => { };
        fixedAction = () => { };

    }

    /// <summary>
    /// 이동 입력 함수
    /// </summary>
    /// <param name="obj"></param>
    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        dir = obj.ReadValue<Vector2>();
        if (InLadder)
        {
            if (dir.y != 0)
            {
                InRunning = false;
                LadderRideing = true;
                animator.SetFloat("Ladder", dir.y);
            }
            else if (!LadderRideing)
            {
                InRunning = true;
            }
            else
            {
                animator.SetFloat("Ladder", dir.y);
            }
        }
        else
        {
            InRunning = true;
        }
        if (dir.x == 0)
        {
            InRunning = false;
        }
    }

    /// <summary>
    /// 이동 작업 함수
    /// </summary>
    void moveActive()
    {
        transform.Translate(MoveSpeed * Time.fixedDeltaTime * dir * Vector2.right);
    }

    /// <summary>
    /// 사다리 작업 함수
    /// </summary>
    void LadderActive()
    {
        transform.Translate(MoveSpeed * Time.fixedDeltaTime * dir * Vector2.up);
    }

    /// <summary>
    /// 점프 입력 합수
    /// </summary>
    /// <param name="obj"></param>
    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ///공중에 있을때는 점프 불가
        if (!InAir)
        {
            InJump = true;
        }

        ///사다리를 타고 있을때는 점프는 안하지만 사다리 타기 종료
        if (LadderRideing)
        {
            LadderRideing = false;
        }
    }

    /// <summary>
    /// 점프 도중에 떨어지기 시작한건지 체크
    /// </summary>
    void JumpActive()
    {
        if (rb.velocity.y < 0.8f)
        {
            animator.SetInteger("Jump", 2);
            action -= JumpActive;
        }
    }
}
