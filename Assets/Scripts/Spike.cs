using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float knockback = 15f;

        
    protected void OnTriggerEnter2D(Collider2D collider) {
        PlayerMovement player = collider.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Hit(gameObject);
        }
    }
    
}
