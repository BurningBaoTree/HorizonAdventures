using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float HP = 30;
    CircleCollider2D collider;
    SpriteRenderer sprite;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        Die();
    }

    void Die()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
