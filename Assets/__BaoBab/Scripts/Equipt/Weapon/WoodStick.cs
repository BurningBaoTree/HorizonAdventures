using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStick : EquiptBase
{
    Animator anim;
    Animator hitAnim;
    CircleCollider2D stickTrigger;


    private void Start()
    {
        anim = GetComponent<Animator>();

        stickTrigger = GetComponent<CircleCollider2D>();

        damage = 10.0f;
        fireSpeed = 0.667f;
    }

    protected override void UseActivate()
    {
        base.UseActivate();
        StartCoroutine(AttackCoolTime());

        if (spRender.transform.localScale == new Vector3(1, 1, 1))
        {
            anim.SetTrigger("Swing");
            anim.SetFloat("AttackSpeed", fireSpeed);
            //spRender.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (spRender.transform.localScale == new Vector3(-1, 1, 1))
        {
            anim.SetTrigger("Swing_Left");
            anim.SetFloat("AttackSpeed", fireSpeed);
            //spRender.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void UseUtillActivate()
    {
        base.UseUtillActivate();

        /*if (spRender.transform.localScale == new Vector3(1, 1, 1))
        {
            anim.SetTrigger("UseUtil");
            //anim.SetFloat("AttackSpeed", fireSpeed);
        }
        else if (spRender.transform.localScale == new Vector3(-1, 1, 1))
        {
            anim.SetTrigger("Swing_Left");
            //anim.SetFloat("AttackSpeed", fireSpeed);
        }*/

        // 추후 던지는 공격 업데이트 예정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Transform child;
            child = transform.GetChild(0);

            if(child != null)
            {
                hitAnim = child.GetComponent<Animator>();
            }

            EnemyBase enemy;
            enemy = collision.GetComponent<EnemyBase>();

            hitAnim.SetTrigger("TargetHit");

            enemy.HP -= damage;

        }
    }

    IEnumerator AttackCoolTime()
    {
        if (stickTrigger == null)
        {
            Debug.Log("트리거 없음");
        }
        stickTrigger.enabled = true;

        yield return new WaitForSeconds(fireSpeed);

        stickTrigger.enabled = false;
    }

}
