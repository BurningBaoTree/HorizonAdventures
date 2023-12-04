using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRock : EnemyBase
{
    Transform player;

    protected override void Awake()
    {
        base.Awake();
        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onFind += () =>
        {
            State = EnemyState.Move;
        };
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        player = GameManager.Inst.PlayerState.transform;
    }

    protected override void MoveInit()
    {
        base.MoveInit();

        elapsedTime = 0;

        anim.SetBool("FindTarget", true);
    }

    protected override void Move()
    {
        transform.position += Time.deltaTime * Vector3.right * MoveDir * moveSpeed;

        if (player.position.x - 2 > transform.position.x)
        {
            MoveDir = 1;
        }
        else if(player.position.x < transform.position.x - 2)
        {
            MoveDir = -1;
        }
    }

    protected override void OnHit()
    {
        base.OnHit();

    }
}
