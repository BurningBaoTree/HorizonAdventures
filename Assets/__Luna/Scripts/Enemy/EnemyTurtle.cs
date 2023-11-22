using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : EnemyBase
{
    /// <summary>
    /// 가시가 밖으로 나와있는지 확인하는 변수
    /// </summary>
    private bool spikesOut = false;

    /// <summary>
    /// wait 상태 지속 시간
    /// </summary>
    private float waitTime = 1.0f;

    /// <summary>
    /// move 상태 지속 시간
    /// </summary>
    private float moveTime = 4.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        spikesOut = false;
    }

    protected override void Wait()
    {
        if (elapsedTime > waitTime)
        {
            State = EnemyState.Move;
        }
    }

    protected override void WaitInit()
    {
        elapsedTime = 0;
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * Vector3.right * MoveDir * moveSpeed;

        if (elapsedTime > moveTime)
        {
            State = EnemyState.Wait;
        }
    }

    protected override void MoveInit()
    {
        elapsedTime = 0;

        MoveDir = -MoveDir;
    }

    protected override void OnHit()
    {
        if (!spikesOut)
        {
            base.OnHit();

            StartCoroutine(SpikesOutIn());
        }
    }

    IEnumerator SpikesOutIn()
    {
        State = EnemyState.Wait;

        invinable = true;

        spikesOut = true;

        anim.SetBool("SpikesOut", spikesOut);

        yield return new WaitForSeconds(5);

        spikesOut = false;

        invinable = false;

        anim.SetBool("SpikesOut", spikesOut);
    }
}
