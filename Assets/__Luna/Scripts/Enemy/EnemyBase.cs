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
    private float maxHealth = 5.0f;

    /// <summary>
    /// 체력 프로퍼티
    /// </summary>
    protected float Health
    {
        get => health;
        set
        {
            if(health != value)
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
                        ChangeWait();
                        onStateUpdate = Wait;
                        break;
                    case EnemyState.Move:
                        ChangeMove();
                        onStateUpdate = Move;
                        break;
                    case EnemyState.Attack:
                        ChangeAttack();
                        onStateUpdate = Attack;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 이동 상태일때 움직일 방향
    /// </summary>
    protected Vector3 moveDir = Vector3.left;

    /// <summary>
    /// 상태 별 변화용 업데이트 델리게이트
    /// </summary>
    System.Action onStateUpdate;

    // 컴포넌트들
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    // < >   ======================================================================================

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
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

    }

    /// <summary>
    /// 죽었을 때 실행할 함수
    /// </summary>
    protected virtual void Die()
    {

    }

    /// <summary>
    /// 상태가 Wait으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void ChangeWait()
    {

    }

    /// <summary>
    /// 상태가 Move으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void ChangeMove()
    {

    }

    /// <summary>
    /// 상태가 Attack으로 바뀌기 전에 실행할 함수
    /// </summary>
    protected virtual void ChangeAttack()
    {

    }

    //===================================================================================

    public void Test_OnHit()
    {
        Health--;
    }
}
