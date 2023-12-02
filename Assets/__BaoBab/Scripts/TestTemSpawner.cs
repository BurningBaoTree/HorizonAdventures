using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestTemSpawner : TestBase
{
    public ItemCode code;

    protected override void Test0(InputAction.CallbackContext context)
    {
        GameObject gameObject1 = ItemSpawner.MakeItem(code, transform.position, false);
    }
}
