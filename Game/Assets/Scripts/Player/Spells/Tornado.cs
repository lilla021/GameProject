using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Spell
{
    [SerializeField]
    float speed;
    [SerializeField]
    float knockupForce;

    // Start is called before the first frame update
    void Start()
    {
        manaCost = 10;
        if (PlayerData.CurrentMana >= manaCost) PlayerData.CurrentMana -= manaCost;
        else {
            PlayerData.IsCasting = false;
            Destroy(gameObject);
        }
        lifespan = 3;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Tornado")) {
            timer += Time.deltaTime;
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
        if (timer >= lifespan) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Debug.Log(collision.name);
            knockUp(collision.GetComponent<Enemy>());
        }
    }

    void knockUp(Enemy enemy) {
        if (enemy.weight < 10) {
            enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockupForce, ForceMode2D.Impulse);
        }
    }

    public void flip() {
        speed *= -1;
    }
}
