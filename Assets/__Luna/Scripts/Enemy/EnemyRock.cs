using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRock : EnemyBase
{
    private bool isFind = false;

    protected override void Awake()
    {
        base.Awake();
        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onFind += () =>
        {
            isFind = true;
            State = EnemyState.Move;
        };
    }

    protected override void WaitInit()
    {
        base.WaitInit();

        elapsedTime = 0;
    }

    protected override void Wait()
    {
        base.Wait();
    }

    protected override void MoveInit()
    {
        base.MoveInit();

        elapsedTime = 0;
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void OnHit()
    {
        base.OnHit();

    }
}
