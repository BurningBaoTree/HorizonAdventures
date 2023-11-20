using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSlime : TestBase
{
    public EnemySlime slime;

    protected override void Test1(InputAction.CallbackContext context)
    {
        slime.Test_OnHit();
    }
}
