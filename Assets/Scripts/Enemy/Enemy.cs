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
    
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Move();
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

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

}
