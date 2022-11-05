using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private LayerMask jumpCheck;
    [SerializeField] private float speed = 5f;

    public bool isAttacking = false;
    
    private States position = States.IdleRight;
    private Vector2 direction;
    private BoxCollider2D bc;
    private float horizontal;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (!isAttacking) rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        if (Input.GetButton("Fire1") && IsGrounded())
            isAttacking = true;
        else if (Input.GetButtonDown("Jump") && IsGrounded() && !isAttacking)
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        UpdateState();
    }

    private void UpdateState()
    {
        if (isAttacking)
        {
            State = position == States.IdleRight ? States.AttackRight : States.AttackLeft;
            isAttacking = false;
        }
        else if (horizontal == 0f) State = position;
        else if (horizontal > 0f)
        {
            State = rb.velocity.y != 0f ? States.JumpRight : States.Right;
            position = States.IdleRight;
        }
        else if (horizontal < 0f)
        {
            State = rb.velocity.y != 0f ? States.JumpLeft : States.Left;
            position = States.IdleLeft;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpCheck);
    }

    private States State
    {
        get => (States) anim.GetInteger("State");
        set => anim.SetInteger("State", (int)value);
    }
    
    public enum States
    {
        IdleRight,
        IdleLeft,
        Right,
        Left,
        JumpRight,
        JumpLeft,
        AttackRight,
        AttackLeft
    }
}
