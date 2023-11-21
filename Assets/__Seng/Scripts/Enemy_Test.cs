using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : MonoBehaviour
{
    public float HP = 30;
    SpriteRenderer sprite;

    private void Awake()
    {
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
