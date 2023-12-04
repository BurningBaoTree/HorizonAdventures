using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRockM : EnemyBase
{
    float waitTime = 1.0f;

    Transform player;

    protected override void OnEnable()
    {
        base.OnEnable();

        player = GameManager.Inst.PlayerState.transform;

        State = EnemyState.Wait;
    }

    protected override void WaitInit()
    {
        base.WaitInit();

        elapsedTime = 0.0f;

        anim.SetBool("FindTarget", false);
    }

    protected override void Wait()
    {
        if (elapsedTime > waitTime)
        {
            State = EnemyState.Move;
        }
    }

    protected override void MoveInit()
    {
        base.MoveInit();

        elapsedTime = 0;

        anim.SetBool("FindTarget", true);

        if (player.position.x > transform.position.x)
        {
            MoveDir = 1;
        }
        else if (player.position.x < transform.position.x)
        {
            MoveDir = -1;
        }
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * Vector3.right * MoveDir * moveSpeed;

        if (player.position.x - 2.5f > transform.position.x)
        {
            MoveDir = 1;
        }
        else if (player.position.x < transform.position.x - 2.5f)
        {
            MoveDir = -1;
        }
    }

    protected override void OnHit()
    {
        base.OnHit();

        State = EnemyState.Wait;
    }

    protected override void Die()
    {
        GameObject small = transform.GetChild(0).gameObject;

        small.transform.parent = null;

        small.SetActive(true);

        base.Die();
    }
}
