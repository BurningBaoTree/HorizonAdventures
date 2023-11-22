using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGost : EnemyBase
{
    // 1. 공중에 떠다닌다.
    // 2. 플레이어쪽으로 온다.
    // 3. 플레이어를 한대 때리거나 플레이어한테 한대 맞으면 사라진다.

    /// <summary>
    /// 플레이어를 찾았는지 확인하는 변수
    /// </summary>
    private bool isFind = false;

    /// <summary>
    /// wait 상태 지속 시간
    /// </summary>
    private float waitTime = 1.0f;

    /// <summary>
    /// move 상태 지속 시간
    /// </summary>
    private float moveTime = 2.0f;

    /// <summary>
    /// 이동 목적지쪽 방향
    /// </summary>
    private Vector3 movePos;

    /// <summary>
    /// 플레이어
    /// </summary>
    private Transform target;

    protected override void OnEnable()
    {
        base.OnEnable();
        isFind = false;
        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onFind += () =>
        {
            elapsedTime = 0;
            isFind = true;
        };
    }

    protected override void Wait()
    {
        if (isFind && elapsedTime > waitTime)
        {
            State = EnemyState.Move;
        }
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * movePos * moveSpeed;

        if (elapsedTime > moveTime)
        {
            StartCoroutine(DisapperCoroutine());
        }
    }

    protected override void WaitInit()
    {
        if (!target)
        {
            target = GameManager.Inst.PlayerEquiped.transform;
        }

        elapsedTime = 0.0f;
    }

    protected override void MoveInit()
    {
        elapsedTime = 0.0f;
        moveSpeed = 5.0f;

        movePos = target.position - transform.position;
        movePos.Normalize();
    }

    protected override void OnHit()
    {
        base.OnHit();

        StartCoroutine(DisapperCoroutine());
    }

    /// <summary>
    /// 사라졌다 나타날때를 표현할 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisapperCoroutine()
    {
        State = EnemyState.Wait;
        coll.enabled = false;

        yield return new WaitForSeconds(0.2f);

        Disapper();

        yield return new WaitForSeconds(0.5f);

        coll.enabled = true;
        Apper();
    }

    /// <summary>
    /// 사라질때 실행할 함수
    /// </summary>
    private void Disapper()
    {
        anim.SetBool("Disappear", true);
    }

    /// <summary>
    /// 나타날때 실행할 함수
    /// </summary>
    private void Apper()
    {
        // 랜덤 방향의 방향벡터 구하기
        Vector2 random = Random.insideUnitCircle.normalized;

        // 플레이어 위치에서 4 떨어진 위치로 이동
        transform.position = (Vector2)target.position + random * 4.0f;

        // 스프라이트 방향 바꾸기
        spriteRenderer.flipX = target.position.x > transform.position.x;

        anim.SetBool("Disappear", false);
    }
}
