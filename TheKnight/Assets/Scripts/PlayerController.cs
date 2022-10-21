using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private LayerMask jumpCheck;
    
    private States position = States.IdleRight;
    private Vector2 direction;
    private Animator anim;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private float horizontal;
    
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        UpdateAnimationState();
    }
    
    private void UpdateAnimationState()
    {
        if (horizontal == 0f) State = position;
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
        JumpLeft
    }
}
