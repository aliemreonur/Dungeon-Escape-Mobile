using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private GameObject _acidPrefab;
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Move()
    {
        
    }

    public void Damage()
    {
        //Debug.Log("Damage");
        anim.SetBool("InCombat", true);
        Health--;
        isHit = true;
        if (Health < 1)
        {
            anim.SetBool("InCombat", false);
            anim.SetTrigger("Death");
            isDead = true;
            Diamond diamondToSpawn = Instantiate(diamond, transform.position, Quaternion.identity);
            if (diamondToSpawn != null)
            {
                diamondToSpawn.gemAmount = gems;
            }
            Destroy(this.gameObject, 1.5f);
        }
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}

