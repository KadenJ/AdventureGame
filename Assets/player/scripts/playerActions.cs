using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerActions : MonoBehaviour
{
    #region basic attack var
    public GameObject attackBox;
    public float attackCooldown;
    public static int dmg;
    #endregion

    #region attack
    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(attackCooldown <= 0)
            {
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
        this.GetComponent<playerMovement>().speed = 8;
        attackCooldown = .5f;
    }
    #endregion

    private void Update()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= 1 * Time.deltaTime;
        }
    }

}
