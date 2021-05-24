using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        //Enemy abstract class already contains a health field.
        //simply setting the idamageable interface property to the value in the inspector
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
        if(Health < 1)
        {
            anim.SetBool("InCombat", false);
            anim.SetTrigger("Death");
            Destroy(this.gameObject, 2.0f);
        }
    }

}
