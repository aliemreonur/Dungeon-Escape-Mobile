using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set;}

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Move()
    {
        base.Move();
    }

    public void Damage()
    {
        //Debug.Log("Damage");
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
        Health--;
        isHit = true;
        if (Health < 1)
        {
            anim.SetBool("InCombat", false);
            anim.SetTrigger("Death");
            isDead = true;
            Destroy(this.gameObject, 1.5f);
        }
    }

}
