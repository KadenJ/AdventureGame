using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public int health = 3;
    public bool isHit = false;
    public bool isShot = false;

    [SerializeField]private float enemyMoveSpeed = 10;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    //public static bool isFacingRight = true;

    private void Start()
    {
       rb = this.gameObject.GetComponent<Rigidbody2D>();
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

        if (isHit == true)
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
        else if (isShot == true)
        {
            rb.velocity = Vector2.zero;
            bool isDone = false;
            if (!isDone)
            {
                if (playerMovement.isFacingRight == true)
                {
                    rb.AddForce(transform.right * projectileCollision.knockback, ForceMode2D.Impulse);
                }
                if (playerMovement.isFacingRight == false)
                {
                    rb.AddForce(transform.right * -projectileCollision.knockback, ForceMode2D.Impulse);
                }
                isDone = true;
            }

            StartCoroutine(Hit());
            //add coroutine to set isHit to false
        }
        else
        {
            //bigger the size faster the move speed but best solution
            rb.velocity = new Vector2(this.transform.localScale.x * enemyMoveSpeed, rb.velocity.y);
        }

        if(this.IsGrounded() == false)
        {
            //isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(.5f);
        isShot = false;
        isHit = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
}
