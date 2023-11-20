using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EquiptBase
{
    Animator anim;
    Animator hitAnim;
    CircleCollider2D swordTrigger;

    private void Start()
    {
        anim = GetComponent<Animator>();

        swordTrigger = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player_Move>();

        damage = 15.0f;
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
        }
        else if (spRender.transform.localScale == new Vector3(-1, 1, 1))
        {
            anim.SetTrigger("Swing_Left");
            anim.SetFloat("AttackSpeed", fireSpeed);
        }
    }

    Player_Move player;
    

    protected override void UseUtillActivate()
    {
        base.UseUtillActivate();
        Vector2 attackRange = new Vector2(3, 1);

        Physics2D.OverlapBox(transform.position, attackRange, 30.0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Transform child;
            child = transform.GetChild(0);

            if (child != null)
            {
                hitAnim = child.GetComponent<Animator>();
            }

            Enemy_Test enemy;
            enemy = collision.GetComponent<Enemy_Test>();

            hitAnim.SetTrigger("TargetHit");

            enemy.HP -= damage;

        }
    }

    IEnumerator AttackCoolTime()
    {
        if (swordTrigger == null)
        {
            Debug.Log("트리거 없음");
        }
        swordTrigger.enabled = true;

        yield return new WaitForSeconds(fireSpeed);

        swordTrigger.enabled = false;
    }

}


