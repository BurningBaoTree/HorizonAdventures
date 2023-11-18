using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponBase : MonoBehaviour
{

    /// <summary>
    /// 무기 이름
    /// </summary>
    public string subweaponName;

    /// <summary>
    /// 무기 설명
    /// </summary>
    public string weaponExplanation;


    public ItemSize itemSize;
    /// <summary>
    /// 데미지
    /// </summary>
    public float damag;

    /// <summary>
    /// 재사용 대기시간
    /// </summary>
    public float timecool;

    /// <summary>
    /// 한번 호출시 사용 횟수
    /// </summary>
    public int useCount;

    /// <summary>
    /// 보조무기 사용 델리게이트
    /// </summary>
    public Action UseSubWeapon;

    /// <summary>
    /// 장착 델리게이트
    /// </summary>
    public Action EquiptSub;

    /// <summary>
    /// 장착 해제 델리게이트
    /// </summary>
    public Action UNEquiptSub;

    public SpriteRenderer subSpRender;
    CapsuleCollider2D capsuleCD2D;
    Rigidbody2D rb2D;

    public bool EndAction = true;

    private void Awake()
    {
        UseSubWeapon = UseSub;
        subSpRender = GetComponent<SpriteRenderer>();
        capsuleCD2D = GetComponent<CapsuleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        EquiptSub = EquiptSubWeapon;
        UNEquiptSub = UNEquiptSubWeapon;
    }

    /// <summary>
    /// 사용 실행 함수
    /// </summary>
    protected virtual void UseSub()
    {

    }

    /// <summary>
    /// 장착 실행 함수
    /// </summary>
    protected virtual void EquiptSubWeapon()
    {
        Debug.Log($"{this.gameObject.name}장착");
        this.transform.localPosition = Vector2.zero;
        this.transform.localRotation = Quaternion.identity;
        rb2D.isKinematic = true;
        capsuleCD2D.enabled = false;
    }

    /// <summary>
    /// 장착 해제 실행 함수
    /// </summary>
    protected virtual void UNEquiptSubWeapon()
    {
        Debug.Log($"{this.gameObject.name}장착 해제");
        rb2D.isKinematic = false;
        capsuleCD2D.enabled = true;
    }
}
