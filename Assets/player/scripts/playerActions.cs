using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerActions : MonoBehaviour
{
    #region animations

    Animator animator;
    int AttackingHash;
    int isFireballHash;
    #endregion

    #region basic attack var
    public GameObject attackBox;
    public float attackCooldown;
    public static int dmg;

    #endregion

    #region projectile vars

    public GameObject fireball;
    public float projectileCooldown;
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        AttackingHash = Animator.StringToHash("Attacking");
        isFireballHash = Animator.StringToHash("isFireball");
    }

    #region attack
    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && animator.GetBool("isFireball") == false)
        {
            bool Attacking = animator.GetBool(AttackingHash);
            if(attackCooldown <= 0)
            {
                animator.SetBool("Attacking", true);
                this.GetComponent<playerMovement>().speed = 0;
                StartCoroutine("attack");
                dmg = 1;
            }
            
        }
        
    }

    IEnumerator attack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        attackBox.SetActive(false);
        animator.SetBool("Attacking", false);
        this.GetComponent<playerMovement>().speed = 8;
        attackCooldown = .5f;
    }
    #endregion

    public void Projectile(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && animator.GetBool("Attacking") == false)
        {
            if(projectileCooldown <= 0)
            {
                animator.SetBool("isFireball", true);
                Debug.Log("fireball");
                Vector2 firebalPos = new Vector2(attackBox.transform.position.x, attackBox.transform.position.y + .3f);
                Instantiate(fireball, firebalPos, Quaternion.identity);
                projectileCooldown = 2f;
            }

        }
    }
    public void projectileEnd()
    {
        animator.SetBool("isFireball", false);
    }

    private void Update()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= 1 * Time.deltaTime;
        }
        if (projectileCooldown > 0)
        {
            projectileCooldown -= 1 * Time.deltaTime;
        }
    }

}
