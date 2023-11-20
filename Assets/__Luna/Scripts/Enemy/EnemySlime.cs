using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyBase
{
    // 1. 슬라임 로직
    // 2. 슬라임은 좌우로만 움직인다.
    // 3. 슬라임은 벽을 만나면 반대 방향으로 바뀐다.
    // 4. 대미지를 받으면 살짝 밀려난다.

    private float waitTime = 3.0f;

    private float moveTime = 6.0f;

    protected override void Wait()
    {
        if(elapsedTime > waitTime)
        {
            State = EnemyState.Move;
        }
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * moveDir * moveSpeed;

        if(elapsedTime > moveTime)
        {
            State = EnemyState.Wait;
        }
    }

    protected override void ChangeWait()
    {
        elapsedTime = 0.0f;
        waitTime = Random.Range(1, 4);
    }

    protected override void ChangeMove()
    {
        elapsedTime = 0.0f;
        moveTime = Random.Range(5, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            moveDir = -moveDir;
        }
    }

    protected override void OnHit()
    {
        base.OnHit();

        Debug.Log($"{gameObject.name}의 체력이 {Health}로 감소했다.");
    }
}
