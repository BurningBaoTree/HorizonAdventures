using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStick : EquiptBase
{
    Animator anim;
    Animator hitAnim;
    CircleCollider2D stickTrigger;

    float speed = 2.0f;
    bool unequipped = false;

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
        unequipped = true;

        UNEquiptThis?.Invoke();

        // 플레이어로 부터 해제
        transform.SetParent(null, true);

        

        if (spRender.transform.localScale == new Vector3(1, 1, 1))
        {
            rb2D.AddForce(speed * Vector2.one, ForceMode2D.Impulse);
        }
        else if (spRender.transform.localScale == new Vector3(-1, 1, 1))
        {
            rb2D.AddForce(speed * Vector2.one, ForceMode2D.Impulse);
        }

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

            Enemy_Test enemy;
            enemy = collision.GetComponent<Enemy_Test>();

            hitAnim.SetTrigger("TargetHit");

            enemy.HP -= damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            speed = 0;
            rb2D.gravityScale = 0;
        }
    }



    protected override void UNEquiptThisGear()
    {
        base.UNEquiptThisGear();

        rb2D.gravityScale = 0.2f;
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
