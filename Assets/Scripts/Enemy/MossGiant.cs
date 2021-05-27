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
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
        Health--;
        isHit = true;
        if (Health < 1 && !isDead)
        {
            anim.SetBool("InCombat", false);
            anim.SetTrigger("Death");
            isDead = true;
            Diamond diamondToSpawn = Instantiate(diamond, transform.position, Quaternion.identity);
            if (diamondToSpawn != null)
            {
                diamondToSpawn.gemAmount = gems;
            }

            //Alternative Way: declare the diamond prefab as a gameobject in the inspector and access its Diamond component
            //GameObject diamondToSpawn = Instantiate(diamond, transform.position, Quaternion.dientity) as GameObject;
            //as GameObject is same as typing (GameObject)Instantiate ... 

            Destroy(this.gameObject, 1.5f);
        }
    }

}
