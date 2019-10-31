using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Enemy"))
        {
            DarkKnight.KnightHP -= PlayerData.AttackStrength;
            Debug.Log("atk");
        }
    }
}
