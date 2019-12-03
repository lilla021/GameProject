using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = (Enemy)col.GetComponent(typeof(Enemy));
            enemy.getHit(PlayerData.AttackStrength);
        }
    }
}
