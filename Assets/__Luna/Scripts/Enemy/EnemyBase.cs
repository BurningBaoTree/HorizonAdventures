using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// 적의 상태 머신
    /// </summary>
    public enum EnemyState
    {
        None,
        Wait,
        Move,
        Attack
    }

    /// <summary>
    /// 무적인지 확인하는 변수
    /// </summary>
    protected bool invinable = false;

    /// <summary>
    /// 경과 시간 체크용
    /// </summary>
    protected float elapsedTime = 0.0f;

    /// <summary>
    /// 체력
    /// </summary>
    private float health = 0.0f;

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHealth = 100.0f;

    /// <summary>
    /// 체력 프로퍼티
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            if(!invinable && health != value)
            {
                health = value;
                if (health > 0)
                {
                    OnHit();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 0.0f;

    /// <summary>
    /// 현재 상태
    /// </summary>
    protected EnemyState state = EnemyState.None;

    /// <summary>
    /// 상태의 변화에 따라 바뀔 상태 프로퍼티
    /// </summary>
    protected EnemyState State
    {
        set
        {
            if (state != value)
            {
                state = value;
                switch (state)
                {
                    case EnemyState.Wait:
                        WaitInit();
                        onStateUpdate = Wait;
                        break;
                    case EnemyState.Move:
                        MoveInit();
                        onStateUpdate = Move;
                        break;
                    case EnemyState.Attack:
                        AttackInit();
                        onStateUpdate = Attack;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 이동 상태일때 움직일 방향 (-1이면 왼쪽으로 이동)
    /// </summary>
    private int moveDir = -1;
    protected int MoveDir
    {
        get => moveDir;
        set
        {
            if(moveDir != value)
            {
                moveDir = value;
                spriteRenderer.flipX = moveDir > 0;
            }
        }
    }

    /// <summary>
    /// 상태 별 변화용 업데이트 델리게이트
    /// </summary>
    System.Action onStateUpdate;

    // 컴포넌트들
    protected Animator anim;
    protected Collider2D coll;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    // < >   ======================================================================================

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        health = maxHealth;
        State = EnemyState.Wait;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        onStateUpdate();
    }

    // <Fuc >   ===================================================================================

    /// <summary>
    /// 잠시 기다리는 상태
    /// </summary>
    protected virtual void Wait()
    {

    }

    /// <summary>
    /// 이동하는 상태
    /// </summary>
    protected virtual void Move()
    {

    }

    /// <summary>
    /// 공격하는 상태
    /// </summary>
    protected virtual void Attack()
    {

    }

    /// <summary>
    /// 피격시 실행할 함수
    /// </summary>
    protected virtual void OnHit()
    {
        anim.ResetTrigger("OnHit");
        anim.SetTrigger("OnHit");

        Debug.Log($"{gameObject.name}의 체력이 {Health}로 감소했다.");
    }

    /// <summary>
    /// 죽었을 때 실행할 함수
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} 죽음!");
        Destroy(gameObject);
    }

    /// <summary>
    /// 상태가 Wait으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void WaitInit()
    {

    }

    /// <summary>
    /// 상태가 Move으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void MoveInit()
    {

    }

    /// <summary>
    /// 상태가 Attack으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void AttackInit()
    {

    }

    //===================================================================================

    public void Test_OnHit()
    {
        Health--;
    }
}
