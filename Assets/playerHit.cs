using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    public LayerMask playerLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            playerMovement.health -= 1;
            playerMovement.playerHit = true;
            //if playerHit == true
            //put player into new layer in playermovement script
        }

    }
}
