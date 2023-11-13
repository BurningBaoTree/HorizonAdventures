using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EquiptBase : MonoBehaviour
{
    public Action UseAction;
    public Action UtillAction;

    public Action EquiptThis;
    public Action UNEquiptThis;

    CapsuleCollider2D capsuleCD2D;
    Rigidbody2D rb2D;

    public Vector2 FixedPosition;

    public string weaponName;
    public string weaponExplanation;
    public float damag;
    public float fireSpeed;
    public int ammoCount;
    public float reLoadTime;

    protected virtual void Awake()
    {
        UseAction = UseActivate;
        UtillAction = UseUtillActivate;
        capsuleCD2D = GetComponent<CapsuleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        EquiptThis = EquiptThisGear;
        UNEquiptThis = UNEquiptThisGear;
    }

    protected virtual void UseActivate()
    {

    }
    protected virtual void UseUtillActivate()
    {

    }
    protected virtual void EquiptThisGear()
    {
        rb2D.isKinematic = true;
        capsuleCD2D.isTrigger = true;
        transform.localPosition = FixedPosition;
    }
    protected virtual void UNEquiptThisGear()
    {
        rb2D.isKinematic = false;
        capsuleCD2D.isTrigger = false;
    }
}
