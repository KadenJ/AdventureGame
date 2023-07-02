using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    #region movement var
    public LayerMask groundLayer;
    public Transform groundCheck;
    private GameObject body;

    private float horizontal;
    public float speed = 8;
    private float jump = 4;
    private int jumpCount;
    public int maxJumps = 2;
    public static bool isFacingRight = true;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        body = GameObject.Find("body");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f && speed > 0)
        {
            Flip();
        }else if (isFacingRight && horizontal < 0f && speed > 0)
        {
            Flip();
        }

        if (IsGrounded())
        {
            jumpCount = maxJumps;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && IsGrounded() || ctx.performed && jumpCount > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpCount -= 1;

        }
        if(ctx.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    public void Shrink(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //crouch
            float crouchHeight = this.gameObject.GetComponent<BoxCollider2D>().size.y * .5f;
            //box collider
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(this.gameObject.GetComponent<BoxCollider2D>().size.x, crouchHeight);
            //body render
            body.transform.localScale = new Vector2(body.transform.localScale.x, crouchHeight);
        }
        if (ctx.canceled)
        {
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(this.gameObject.GetComponent<BoxCollider2D>().size.x, this.gameObject.GetComponent<BoxCollider2D>().size.y /.5f);
            body.transform.localScale = new Vector2(body.transform.localScale.x, 1);
        }

        
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
