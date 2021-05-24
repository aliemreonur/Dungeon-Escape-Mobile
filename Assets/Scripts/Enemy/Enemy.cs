using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int health;
    [SerializeField] protected int gems;

    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;

    protected bool isHit;
    protected Player player;
    
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
          
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }
        Move();
        //Death();

    }
    public virtual void Move()
    {
        if (currentTarget == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }

        if(!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        //float distance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 distance = player.transform.position - transform.position;
        if (distance.x > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        else if (distance.x <= 2.0f && anim.GetBool("InCombat"))
        {
            if (distance.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (distance.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        
    }

    public virtual void Death()
    {
        if(health < 1)
        {
            anim.SetBool("Death", true);
        }
    }
}
