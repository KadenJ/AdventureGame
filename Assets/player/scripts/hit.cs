using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    private GameObject enemy;
    private Rigidbody2D enemyRb;
    public static float knockback = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            
            enemy = collision.gameObject;
            enemyRb = enemy.GetComponent<Rigidbody2D>();
            enemy.GetComponent<enemyScript>().isHit = true;
            Debug.Log("hit enemy");
            enemy.GetComponent<enemyScript>().health -= playerActions.dmg;
            
        }
        
    }

}
