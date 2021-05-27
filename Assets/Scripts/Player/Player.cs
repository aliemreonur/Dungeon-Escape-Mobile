using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    Rigidbody2D _rb2D;
    SpriteRenderer _spriteRenderer;
    //[SerializeField] private int _diamonds;
    public int diamonds;

    private bool _isAlive;
    public bool isAlive
    {
        get { return _isAlive; }
        private set { _isAlive = value; }
    }

    private SpriteRenderer _swordArcSprite;

    private PlayerAnimation _anim;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jump = 5f;
    [SerializeField] private bool _isGrounded, _facingLeft;
    [SerializeField] private int health;
    private bool _resetJumpNeed = false;

    public int Health { get; set; }

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
        Health = 4;

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
        float horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        Flip(horizontalInput);

        _rb2D.velocity = new Vector2(horizontalInput * _speed, _rb2D.velocity.y);
        _anim.Move(horizontalInput);

        if ((CrossPlatformInputManager.GetButtonDown("B_Button")) && _isGrounded)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jump);
            _anim.Jump(true);
            _isGrounded = false;
            _resetJumpNeed = true;
            StartCoroutine(ResetJumpRoutine());
        }
    }

    //float horizontalInput = Input.GetAxisRaw("Horizontal");
    //Input.GetKeyDown(KeyCode.Space) ||


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
        if( CrossPlatformInputManager.GetButtonDown("A_Button") && _isGrounded)
        {
            _anim.Attack();
        }
    }
    //Input.GetMouseButtonDown(0) ||

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

    public void Damage()
    {
        if(Health <1)
        {
            return; //return if already dead
        }
        _anim.Damage(Health);
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if(Health <1)
        {
            _anim.Death();
            _isAlive = false;
            
        }

    }

    public void AddDiamond(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    IEnumerator ResetJumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeed = false;
    }

}
