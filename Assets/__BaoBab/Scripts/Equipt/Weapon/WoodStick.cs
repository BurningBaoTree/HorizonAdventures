using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStick : EquiptBase
{
    Animator anim;
    CircleCollider2D stickTrigger;


    private void Start()
    {
        anim = GetComponent<Animator>();
        stickTrigger = GetComponent<CircleCollider2D>();

        StopAllCoroutines();

        // 대미지, 공격속도
        damage = 10.0f;
        fireSpeed = 0.667f;
    }

    private void Update()
    {
       
    }
    protected override void UseActivate()
    {
        base.UseActivate();

        StartCoroutine(AttackCoolTime());

        if (spRender.transform.localScale == new Vector3(1, 1, 1))
        {
            anim.SetTrigger("Swing");
            //spRender.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (spRender.transform.localScale == new Vector3(-1, 1, 1))
        {
            anim.SetTrigger("Swing_Left");
            //spRender.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyBase enemy;
            enemy = collision.GetComponent<EnemyBase>();

            enemy.HP -= damage;

        }
    }

    IEnumerator AttackCoolTime()
    {
        stickTrigger.enabled = true;

        yield return new WaitForSeconds(fireSpeed);

        stickTrigger.enabled = false;
    }


}
