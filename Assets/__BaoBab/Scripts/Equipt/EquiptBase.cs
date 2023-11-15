using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptBase : MonoBehaviour
{
    /// <summary>
    /// 좌클릭 델리게이트
    /// </summary>
    public Action UseAction;
    public Action StopUseAction;

    /// <summary>
    /// 우클릭 델리게이트
    /// </summary>
    public Action UtillAction;
    public Action StopUtillAction;

    /// <summary>
    /// 장착 델리게이트
    /// </summary>
    public Action EquiptThis;

    /// <summary>
    /// 장착 해제 델리게이트
    /// </summary>
    public Action UNEquiptThis;



    CapsuleCollider2D capsuleCD2D;
    Rigidbody2D rb2D;
    public SpriteRenderer spRender;

    /// <summary>
    /// 무기 이름
    /// </summary>
    public string weaponName;

    /// <summary>
    /// 무기 설명
    /// </summary>
    public string weaponExplanation;

    /// <summary>
    /// 데미지
    /// </summary>
    public float damag;

    /// <summary>
    /// 공격 속도 / 사용 시간
    /// </summary>
    public float fireSpeed;

    /// <summary>
    /// 사용 횟수
    /// </summary>
    public int ammoCount;

    /// <summary>
    /// 재장전 / 사용 후 쿨타임
    /// </summary>
    public float reLoadTime;

    protected virtual void Awake()
    {
        UseAction = UseActivate;
        UtillAction = UseUtillActivate;
        capsuleCD2D = GetComponent<CapsuleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        EquiptThis = EquiptThisGear;
        UNEquiptThis = UNEquiptThisGear;
        spRender = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 마우스 좌클릭 사용시 실행될 함수
    /// </summary>
    protected virtual void UseActivate()
    {
        Debug.Log($"{this.gameObject.name}을 사용했다. 효과는 굉장했다.");
    }

    /// <summary>
    /// 마우스 우클릭 사용시 실행될 함수
    /// </summary>
    protected virtual void UseUtillActivate()
    {
        Debug.Log($"{this.gameObject.name}의 보조기능을 사용했다. 효과는 별거 없었다.");
    }

    /// <summary>
    /// 장착시 실행될 함수
    /// </summary>
    protected virtual void EquiptThisGear()
    {
        Debug.Log($"{this.gameObject.name}장착");
        this.transform.localPosition = Vector2.zero;
        this.transform.localRotation = Quaternion.identity;
        rb2D.isKinematic = true;
        capsuleCD2D.enabled = false;
    }

    /// <summary>
    /// 장착 해제시 실행될 함수
    /// </summary>
    protected virtual void UNEquiptThisGear()
    {
        Debug.Log($"{this.gameObject.name}장착 해제");
        rb2D.isKinematic = false;
        capsuleCD2D.enabled = true;
    }
}
