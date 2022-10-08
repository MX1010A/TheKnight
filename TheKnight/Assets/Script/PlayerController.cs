using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private States position = States.IdleRight;
    private Vector2 direction;
    private Animator animator;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal");
        
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));

        if(direction.x == 0) State = position;
        else if (direction.x > 0)
        {
            State = States.Right;
            position = States.IdleRight;
        }
        else if (direction.x < 0)
        {
            State = States.Left;
            position = States.IdleLeft;
        }
    }
    
    private States State
    {
        get => (States) animator.GetInteger("State");
        set => animator.SetInteger("State", (int)value);
    }
    
    public enum States
    {
        IdleRight,
        IdleLeft,
        Right,
        Left
    }
}
