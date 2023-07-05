using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    /*every enemy needs
     * death anim
     *      -dead bool
     *      -animation event
     * attack anim
     *      -attack bool
     * rigid body
     * ground layer chack
     * player layer check
     * 
     * add different health amount somehow
    */

    public int health = 5;
    public bool isHit = false;
    public bool isShot = false;

    [SerializeField]private float enemyMoveSpeed = 10;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform playerCheck;
    public LayerMask playerLayer;
    //public static bool isFacingRight = true;

    Animator animator;
    int isDeadHash;
    int attackHash;

    private void Start()
    {
       rb = this.gameObject.GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       isDeadHash = Animator.StringToHash("isDead");
       attackHash = Animator.StringToHash("isAttacking");  
    }

    private void Update()
    {
        

        if (health <= 0)
        {
            //Destroy(this.gameObject);
            bool isDead = animator.GetBool(isDeadHash);
            animator.SetBool("dead", true);
        }
    }

    public void die()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        //staff knockback
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
        //fireball knockback
        else if (isShot == true)
        {
            rb.velocity = Vector2.zero;
            bool isDone = false;
            if (!isDone)
            {
                //if velocity < 0, knockback push left
                if (projectileCollision.PVelocity > 0)
                {
                    rb.AddForce(transform.right * projectileCollision.knockback, ForceMode2D.Impulse);
                    
                }
                //if velocity > 0, knockback push right
                if (projectileCollision.PVelocity < 0)
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
            //left right movement
            //bigger the size faster the move speed but best solution
            rb.velocity = new Vector2(this.transform.localScale.x * enemyMoveSpeed, rb.velocity.y);
        }

        //when to turn
        if(this.IsGrounded() == false)
        {
            //isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if(this.DetectPlayer() == true)
        {
            //attack(15);
            //attack bool = true
            animator.SetBool("attack", true);
        }
        else
        {
            //patrol(5);
            animator.SetBool("attack", false);
        }
        
    }

    private void attack(float attackSpeed)
    {
        enemyMoveSpeed = attackSpeed;
    }

    private void patrol(float walkSpeed)
    {
        enemyMoveSpeed = walkSpeed;
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

    private bool DetectPlayer()
    {
        return Physics2D.OverlapBox(playerCheck.position, new Vector2(4,1),0,playerLayer);
    }
}
