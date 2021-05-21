using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Player animation could not get the animator ");
        }
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jump", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnim");
    }
}
