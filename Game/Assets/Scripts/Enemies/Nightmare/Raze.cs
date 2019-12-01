using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raze : Enemy
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("raze");
            PlayerController player = (PlayerController)collision.GetComponent(typeof(PlayerController));
            player.getHit(3);
        }
    }
    protected override void Move()
    {

    }
    protected override void Death()
    {

    }
    protected override void Attack()
    {

    }
}
