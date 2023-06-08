using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public int health = 3;
    public bool isHit = false;

    [SerializeField]private float enemyMoveSpeed = 10;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public static bool isFacingRight = true;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(isHit == true)
        {
            rb.velocity = Vector2.zero;
            bool isDone = false;
            if (!isDone)
            {
                if (playerMovement.isFacingRight == true)
                {
                    rb.AddForce(transform.right * hit.knockback, ForceMode2D.Impulse);
                }
                if (playerMovement.isFacingRight == false)
                {
                    rb.AddForce(transform.right * -hit.knockback, ForceMode2D.Impulse);
                }
                isDone = true;
            }

            StartCoroutine(Hit());
            //add coroutine to set isHit to false
        }
        else if (isFacingRight && !isHit)
        {
            //rb.AddForce(transform.right * enemyMoveSpeed);
            rb.velocity = new Vector2(1 * enemyMoveSpeed, rb.velocity.y);
        }
        else if(!isFacingRight && !isHit)
        {
            //rb.AddForce(transform.right * -enemyMoveSpeed);
            rb.velocity = new Vector2(-1 * enemyMoveSpeed, rb.velocity.y);
        }

        if(IsGrounded() == false)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(.5f);
        isHit = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
