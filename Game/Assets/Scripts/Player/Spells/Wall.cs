using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        manaCost = 25;
        PlayerData.CurrentMana -= manaCost;
        lifespan = 5;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wall")) {
            timer += Time.deltaTime;
        }
        if (timer >= lifespan) Destroy(gameObject);
    }
}
