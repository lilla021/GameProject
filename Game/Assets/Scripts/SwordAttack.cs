using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
        //DarkKnight dark;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject darkKnight = GameObject.Find("DarkKnight");
       // dark = darkKnight.GetComponent<DarkKnight>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if(col.name == "DarkKnight")
            {
                DarkKnight.KnightHP -= (PlayerData.AttackStrength - col.gameObject.GetComponent<DarkKnight>().KnightDefense);              
            }
            else if(col.name == "Wolf")
            {
                Debug.Log("atk wolf");
                Wolf.WolfHP -= PlayerData.AttackStrength;
            }
        }
    }
}
