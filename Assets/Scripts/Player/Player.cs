using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb2D;
    SpriteRenderer _spriteRenderer;

    private SpriteRenderer _swordArcSprite;

    private PlayerAnimation _anim;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jump = 5f;
    [SerializeField] private bool _isGrounded, _facingLeft;
    private bool _resetJumpNeed = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if(_rb2D == null)
        {
            Debug.Log("Player could not get the rigidbody dude");
        }
        _anim = GetComponent<PlayerAnimation>();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            Debug.LogError("Sprite renderer of player is null");
        }

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GroundCheck();
        Attack();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);

        _rb2D.velocity = new Vector2(horizontalInput * _speed, _rb2D.velocity.y);
        _anim.Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {

            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jump);
            _anim.Jump(true);
            _isGrounded = false;
            _resetJumpNeed = true;
            StartCoroutine(ResetJumpRoutine());
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, 0.75f, 1<<8);
        Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.green);

        if (hit2D.collider != null)
        {
            if (!_resetJumpNeed)
            {
                _isGrounded = true;
                _anim.Jump(false);
            }
        }
    }

    private void Flip(float hInput)
    {
        if (hInput > 0)
        {
            _facingLeft = false;
            _spriteRenderer.flipX = false;
            FlipSword();
        }
        else if (hInput < 0)
        {
            _facingLeft = true;
            _spriteRenderer.flipX = true;
            FlipSword();
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _anim.Attack();
        }
    }

    private void FlipSword()
    {
        if(_facingLeft)
        {
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else
        {
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;


            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }

    }

    IEnumerator ResetJumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeed = false;
    }
}
