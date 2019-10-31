using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSwordDmg : MonoBehaviour
{
    DarkKnight dark;
    // Start is called before the first frame update
    void Start()
    {
        dark = GetComponentInParent<DarkKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {          
            PlayerData.CurrentHP -= dark.KnightAtk;
            Debug.Log(PlayerData.CurrentHP);
        }
    }
}
