using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Spell
{
    private void Awake()
    {
        AudioManager.PlayMusic("wall");
    }
    // Start is called before the first frame update
    void Start()
    {
        manaCost = 25;
        if (PlayerData.CurrentMana >= manaCost) PlayerData.CurrentMana -= manaCost;
        else {
            PlayerData.IsCasting = false;
            Destroy(gameObject);
        }
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
