using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCollision : MonoBehaviour
{
    public float projectileSpeed = 1000;
    private Rigidbody2D rb;
    private bool isDone = false;

    private GameObject enemy;
    private Rigidbody2D enemyRb;
    public static float knockback = 1;
    public static float PVelocity;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (isDone == false)
        {
            //direction to shoot projectile
            if (playerMovement.isFacingRight)
            {
                rb.velocity = new Vector2(rb.transform.right.x * projectileSpeed * Time.fixedDeltaTime, 0);
            }
            else
            {
                rb.velocity = new Vector2(rb.transform.right.x * -projectileSpeed * Time.fixedDeltaTime, 0);
            }
            isDone = true;
            PVelocity = rb.velocity.x;
        }

        Destroy(this.gameObject, 5f);

        //Debug.Log(this.GetComponent<Rigidbody2D>().velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            enemy = collision.gameObject;
            enemyRb = enemy.GetComponent<Rigidbody2D>();
            enemy.GetComponent<enemyScript>().isShot = true; //gets error?
            Debug.Log("hit enemy");
            enemy.GetComponent<enemyScript>().health -= playerActions.dmg;

        }

        if(collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
