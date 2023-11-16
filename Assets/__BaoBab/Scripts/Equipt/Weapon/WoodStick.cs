using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStick : EquiptBase
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
       
    }
    protected override void UseActivate()
    {
        base.UseActivate();


        if (!spRender.flipX)
        {
            anim.SetTrigger("Swing");
        }
        else if(spRender.flipX)
        {
            anim.SetTrigger("Swing_Left");
        }

    }

    protected override void UseUtillActivate()
    {
        base.UseUtillActivate();


    }

    protected override void EquiptThisGear()
    {
        base.EquiptThisGear();

        if(spRender.flipX)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -27.47f);
        }
    }
}
