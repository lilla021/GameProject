using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpell : Spell
{
    [SerializeField]
    float attack;
    [SerializeField]
    float speed;
    Camera cam;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        manaCost = 35;
        if (PlayerData.CurrentMana >= manaCost) PlayerData.CurrentMana -= manaCost;
        else {
            PlayerData.IsCasting = false;
            Destroy(gameObject);
        }
        lifespan = 5;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        direction = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Arrow")) {
            transform.right = direction;
            timer += Time.deltaTime;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (timer >= lifespan) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().GetMagicHit(attack);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(gameObject);
        }
    }
}
