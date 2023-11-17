using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float HP = 30;

    private void Start()
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
