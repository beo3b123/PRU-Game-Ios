using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator am;
    private SpriteRenderer sprite;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private Vector2 rollingDir;
    private float rollingTime = 0.2f;
    private float rollingCoolDown = 1f;



    [SerializeField] private float speed = 7f;
    [SerializeField] private float rollingSpeed = 16f;
    [SerializeField] private float jumpspeed = 40f;

    private enum MovementState {idle, running, jumping, falling };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        moveLeft = false;
        moveRight = false;
    }
    public void PointerDownLeft()
    {
        moveLeft = true;
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
    }
    public void PointerDownRight()
    {
        moveRight = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }
    // Update is called once per frame

    void Update()
    {
        MovementPlayer();
        UpdateAnimationState();
        
    }
    private void MovementPlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
        }

        else if (moveRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }
    }
    public void Jump()
    {
        if (rb.velocity.y == 0)
        {
            rb.velocity = Vector2.up * jumpspeed;
        }
    }


    private void UpdateAnimationState()
    {
        MovementState state;
        if (horizontalMove < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }

        else if (horizontalMove > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -1f)
        {
            state = MovementState.falling;
        }

        am.SetInteger("state", (int)state);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }


}
