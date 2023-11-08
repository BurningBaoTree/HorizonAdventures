using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptBase : MonoBehaviour
{
    public Action UseAction;
    public Action UtillAction;

    private void Awake()
    {
        UseAction = UseActivate;
        UtillAction = UseUtillActivate;
    }

    protected virtual void UseActivate()
    {

    }
    protected virtual void UseUtillActivate()
    {

    }
}
