using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyBase
{
    // 1. 슬라임 로직
    // 2. 슬라임은 좌우로만 움직인다.
    // 3. 슬라임은 벽을 만나면 반대 방향으로 바뀐다.
    // 4. 대미지를 받으면 살짝 밀려난다.

    /// <summary>
    /// wait 상태 지속 시간
    /// </summary>
    private float waitTime;

    /// <summary>
    /// move 상태 지속 시간
    /// </summary>
    private float moveTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            MoveDir = -MoveDir;
        }
    }

    protected override void Wait()
    {
        if(elapsedTime > waitTime)
        {
            State = EnemyState.Move;
        }
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * Vector3.right * MoveDir * moveSpeed;

        if(elapsedTime > moveTime)
        {
            State = EnemyState.Wait;
        }
    }

    protected override void WaitInit()
    {
        elapsedTime = 0.0f;
        waitTime = Random.value * 2.0f;
    }

    protected override void MoveInit()
    {
        elapsedTime = 0.0f;
        moveTime = Random.Range(5, 10);
    }

    protected override void OnHit()
    {
        base.OnHit();

        if(state == EnemyState.Move)
        {
            State = EnemyState.Wait;
        }

        Debug.Log($"{gameObject.name}의 체력이 {Health}로 감소했다.");
    }
}
